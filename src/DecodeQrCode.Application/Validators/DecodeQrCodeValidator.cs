using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace DecodeQrCode.Application.Validators;

public class DecodeQrCodeValidator : IDecodeQrCodeValidator
{
    private readonly QrCodeSettings _qrCodeSettings;

    public DecodeQrCodeValidator(IOptions<QrCodeSettings> qrCodeSettings)
    {
        _qrCodeSettings = qrCodeSettings.Value;
    }

    public void Validate(QrCodeDTO qrCodeDTO)
    {
        if (qrCodeDTO.MerchantAccountInformation!.URL is not null)
        {
            HasMinimumEntropy(qrCodeDTO.MerchantAccountInformation.URL);

            VerifyHomologationUrl(qrCodeDTO.MerchantAccountInformation!.URL);
        }
    }

    private void HasMinimumEntropy(string url)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(url);
        
        int totalBits = bytes.Length * 8;

        if (totalBits >= _qrCodeSettings.UriMinumumBitLength)
            throw new ServiceException("A URL deve ter tamanho mínimo de 120 bits aleatórios", HttpStatusCode.BadRequest);
    }

    private void VerifyHomologationUrl(string url)
    {
        if (_qrCodeSettings.AcceptsHomologationCode)
            return;

        Uri uri = new(url);

        if (uri.Host.EndsWith(_qrCodeSettings.HomologationPrefix))
            throw new ServiceException("URL de homologação não permitida", HttpStatusCode.BadRequest);
    }
}
