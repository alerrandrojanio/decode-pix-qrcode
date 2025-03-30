using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Enum;
using DecodeQrCode.Domain.Interfaces;

namespace DecodeQrCode.Application.Services;

public class DecodeQrCodeService : IDecodeQrCodeService
{
    private readonly IDecodeService _decodeService;
    private readonly IDecodeQrCodeIntegrationService _decodeQrCodeIntegrationService;

    public DecodeQrCodeService(IDecodeService decodeService, IDecodeQrCodeIntegrationService decodeQrCodeIntegrationService)
    {
        _decodeService = decodeService;
        _decodeQrCodeIntegrationService = decodeQrCodeIntegrationService;
    }

    public void DecodeQrCode(DecodeQrCodeDTO decodeQrCodeDTO)
    {
        QrCodeDTO qrCodeDTO =  _decodeService.DecodeQrCode(decodeQrCodeDTO.QrCode);

        if (string.IsNullOrEmpty(qrCodeDTO.MerchantAccountInformation!.URL))
        {
            qrCodeDTO.Type = QrCodeType.STATIC;
        }
        else
        {
            _decodeQrCodeIntegrationService.DecodeQrCode(qrCodeDTO);
        }
    }
}
