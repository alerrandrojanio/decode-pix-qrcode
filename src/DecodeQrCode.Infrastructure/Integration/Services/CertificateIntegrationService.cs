using DecodeQrCode.Domain.DTOs.Certificate;
using DecodeQrCode.Domain.Extensions;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Net.Security;
using System.Net.Sockets;
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
        Uri uri = new(url.AddSecurityPrefix());

        string cacheKey = CacheExtensions.GenerateKey<ServerCertificateDTO>(nameof(uri), uri.Host);

        IDatabase cacheDatabase = _redisConnection.GetDatabase();

        string? cachedData = cacheDatabase.StringGet(cacheKey);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<ServerCertificateDTO>(cachedData);

        using TcpClient client = new();
        
        await client.ConnectAsync(uri.Host, 443);

        using SslStream sslStream = new(client.GetStream(), false,
            (sender, certificate, chain, sslPolicyErrors) => true);

        await sslStream.AuthenticateAsClientAsync(uri.Host);

        ServerCertificateDTO? serverCertificateDTO = null;

        if (sslStream.RemoteCertificate is X509Certificate2 certificate)
        {
            serverCertificateDTO = new() 
            {
                Issuer = certificate.Issuer,
                Subject = certificate.Subject,
                ValidFrom = certificate.NotBefore,
                ValidUntil = certificate.NotAfter,
                Thumbprint = certificate.Thumbprint,
                Protocol = sslStream.SslProtocol
            };

            cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(serverCertificateDTO), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));
        }

        return serverCertificateDTO;
    }
}
