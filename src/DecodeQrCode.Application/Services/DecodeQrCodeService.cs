using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Enum;
using DecodeQrCode.Domain.Interfaces;

namespace DecodeQrCode.Application.Services;

public class DecodeQrCodeService : IDecodeQrCodeService
{
    private readonly IDecodeService _decodeService;
    private readonly IDecodeQrCodeIntegrationService _decodeQrCodeIntegrationService;
    private readonly IDecodeQrCodeValidator _decodeQrCodeValidator;

    public DecodeQrCodeService(IDecodeService decodeService, IDecodeQrCodeIntegrationService decodeQrCodeIntegrationService, IDecodeQrCodeValidator decodeQrCodeValidator)
    {
        _decodeService = decodeService;
        _decodeQrCodeIntegrationService = decodeQrCodeIntegrationService;
        _decodeQrCodeValidator = decodeQrCodeValidator;
    }

    public void DecodeQrCode(DecodeQrCodeDTO decodeQrCodeDTO)
    {
        QrCodeDTO qrCodeDTO =  _decodeService.DecodeQrCode(decodeQrCodeDTO.QrCode);

        _decodeQrCodeValidator.Validate(qrCodeDTO);

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
