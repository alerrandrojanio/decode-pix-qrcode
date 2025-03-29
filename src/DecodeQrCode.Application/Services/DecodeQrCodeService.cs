using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Integration.Services;

namespace DecodeQrCode.Application.Services;

public class DecodeQrCodeService : IDecodeQrCodeService
{
    private readonly DecodeQrCodeIntegrationService _decodeQrCodeIntegrationService;

    public DecodeQrCodeService(DecodeQrCodeIntegrationService decodeQrCodeIntegrationService)
    {
        _decodeQrCodeIntegrationService = decodeQrCodeIntegrationService;
    }

    public void DecodeQrCode(DecodeQrCodeDTO decodeQrCodeDTO)
    {
        throw new System.NotImplementedException();
    }
}
