using DecodeQrCode.Domain.DTOs.Certificate;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Extensions;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using DecodeQrCode.Infrastructure.Resources;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace DecodeQrCode.Infrastructure.Integration.Services;

public class CertificateIntegrationService : ICertificateIntegrationService
{
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly CacheSettings _cacheSettings;

    public CertificateIntegrationService(IConnectionMultiplexer redisConnection, IOptions<CacheSettings> cacheSettings)
    {
        _redisConnection = redisConnection;
        _cacheSettings = cacheSettings.Value;
    }

    public async Task<ServerCertificateDTO?> GetServerCertificate(string url)
    {
        try
        {
            Uri uri = new(url.AddSecurityPrefix());

            string cacheKey = CacheExtensions.GenerateKey<ServerCertificateDTO>(nameof(uri), uri.Host);

            IDatabase cacheDatabase = _redisConnection.GetDatabase();

            string? cachedData = cacheDatabase.StringGet(cacheKey);

            if (cachedData is not null)
                return JsonSerializer.Deserialize<ServerCertificateDTO>(cachedData);

            using TcpClient client = new();

            await client.ConnectAsync(uri.Host, 443);

            using SslStream sslStream = new(client.GetStream(), false, ValidateCertificateChain);
            await sslStream.AuthenticateAsClientAsync(uri.Host);

            ServerCertificateDTO? serverCertificateDTO = null;

            if (sslStream.RemoteCertificate is X509Certificate2 certificate)
            {
                serverCertificateDTO = new()
                {
                    Issuer = certificate.Issuer,
                    Subject = certificate.Subject,
                    CommonName = certificate.GetNameInfo(X509NameType.SimpleName, false),
                    AlternativeNames = GetSubjectAlternativeNames(certificate),
                    ValidFrom = certificate.NotBefore,
                    ValidUntil = certificate.NotAfter,
                    Thumbprint = certificate.Thumbprint,
                    Protocol = sslStream.SslProtocol
                };

                cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(serverCertificateDTO), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));
            }

            return serverCertificateDTO;
        }
        catch (Exception ex)
        {
            throw new DecodeException(InfrastructureMessage.Certificate_Get_Fail, ex, HttpStatusCode.BadRequest);
        }
    }

    private static bool ValidateCertificateChain(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
    {
        if (sslPolicyErrors == SslPolicyErrors.None)
            return true;

        if (chain is null || certificate is null)
            return false;

        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

        bool isValid = chain.Build(new X509Certificate2(certificate));

        if (!isValid)
            throw new ServiceException(InfrastructureMessage.Certificate_Invalid, HttpStatusCode.BadRequest);

        return isValid;
    }

    private static List<string> GetSubjectAlternativeNames(X509Certificate2 cert)
    {
        string objectIdentifier = "2.5.29.17";
        List<string> result = new();

        foreach (X509Extension extension in cert.Extensions)
        {
            if (extension.Oid?.Value == objectIdentifier)
            {
                AsnEncodedData asnData = new(extension.Oid, extension.RawData);
                
                string[] parts = asnData.Format(false).Split(',');

                foreach (string part in parts)
                {
                    string? trimmed = part.Trim();
                    
                    if (trimmed.StartsWith("DNS Name=", StringComparison.OrdinalIgnoreCase))
                        result.Add(trimmed["DNS Name=".Length..].Trim());
                }
            }
        }

        return result;
    }
}
