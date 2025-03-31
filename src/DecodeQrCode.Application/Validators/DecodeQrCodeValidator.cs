using DecodeQrCode.Application.Resources;
using DecodeQrCode.Domain.DTOs.Certificate;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Authentication;
using System.Text;

namespace DecodeQrCode.Application.Validators;

public class DecodeQrCodeValidator : IDecodeQrCodeValidator
{
    private readonly ICertificateIntegrationService _certificateIntegrationService;
    private readonly QrCodeSettings _qrCodeSettings;

    public DecodeQrCodeValidator(ICertificateIntegrationService certificateIntegrationService, IOptions<QrCodeSettings> qrCodeSettings)
    {
        _certificateIntegrationService = certificateIntegrationService;
        _qrCodeSettings = qrCodeSettings.Value;
    }

    public async Task Validate(QrCodeDTO qrCodeDTO)
    {
        if (qrCodeDTO.MerchantAccountInformation!.URL is not null)
        {
            ValidateUrlMinumumBitSize(qrCodeDTO.MerchantAccountInformation.URL);

            ValidateHomologationUrl(qrCodeDTO.MerchantAccountInformation!.URL);

            ServerCertificateDTO? serverCertificateDTO = await _certificateIntegrationService.GetServerCertificate(qrCodeDTO.MerchantAccountInformation!.URL);

            ValidateCertificate(serverCertificateDTO);
        }
    }

    private void ValidateUrlMinumumBitSize(string url)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(url);
        
        int totalBits = bytes.Length * 8;

        if (totalBits < _qrCodeSettings.UrlMinumumBitSize)
            throw new ServiceException(ApplicationMessage.Validate_URL_MinumumBitSize, HttpStatusCode.BadRequest);
    }

    private void ValidateHomologationUrl(string url)
    {
        if (_qrCodeSettings.AcceptsHomologationCode)
            return;

        Uri uri = new(url);

        if (uri.Host.EndsWith(_qrCodeSettings.HomologationPrefix))
            throw new ServiceException(ApplicationMessage.Validate_URL_Homologation, HttpStatusCode.BadRequest);
    }

    private void ValidateCertificate(ServerCertificateDTO? serverCertificateDTO)
    {
        if (serverCertificateDTO is null)
            throw new ServiceException(ApplicationMessage.Validate_ServerCertificate_NotFound, HttpStatusCode.BadRequest);
        
        if (serverCertificateDTO.ValidUntil < DateTime.Now)
            throw new ServiceException(ApplicationMessage.Validate_ServerCertificate_Expired, HttpStatusCode.BadRequest);

        if (serverCertificateDTO.Protocol is not SslProtocols.Tls12 or SslProtocols.Tls13)
            throw new ServiceException(ApplicationMessage.Validate_ServerCertificate_Protocol, HttpStatusCode.BadRequest);
    }
}
