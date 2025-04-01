using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.DTOs.Decode.Response;
using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Enums;
using DecodeQrCode.Domain.Interfaces;
using Mapster;

namespace DecodeQrCode.Application.Services;

public class DecodeQrCodeService : IDecodeQrCodeService
{
    private readonly IDecodeService _decodeService;
    private readonly IDecodeQrCodeIntegrationService _decodeQrCodeIntegrationService;
    private readonly IDecodeQrCodeValidator _decodeQrCodeValidator;
    private readonly IJKUValidator _jkuValidator;

    public DecodeQrCodeService(IDecodeService decodeService, IDecodeQrCodeIntegrationService decodeQrCodeIntegrationService, IDecodeQrCodeValidator decodeQrCodeValidator, IJKUValidator jkuValidator)
    {
        _decodeService = decodeService;
        _decodeQrCodeIntegrationService = decodeQrCodeIntegrationService;
        _decodeQrCodeValidator = decodeQrCodeValidator;
        _jkuValidator = jkuValidator;
    }

    public async Task<DecodeQrCodeResponseDTO?> DecodeQrCode(DecodeQrCodeDTO decodeQrCodeDTO)
    {
        DecodeQrCodeResponseDTO? decodeQrCodeResponseDTO = null;

        QrCodeDTO qrCodeDTO = _decodeService.DecodeQrCode(decodeQrCodeDTO.QrCode);

        await _decodeQrCodeValidator.Validate(qrCodeDTO);

        JWSDTO? jws = null;

        if (!string.IsNullOrEmpty(qrCodeDTO.MerchantAccountInformation!.URL))
        {
            jws = await _decodeQrCodeIntegrationService.DecodeQrCode(qrCodeDTO) ?? throw new Exception();

            JKUDTO? jku = await _decodeQrCodeIntegrationService.GetJKU(jws) ?? throw new Exception();

            _jkuValidator.Validate(jku);

            qrCodeDTO.Type = jws?.Payload?.Calendar?.DueDate is null ? QrCodeType.IMMEDIATE : QrCodeType.DUEDATE;
        }
        else
            qrCodeDTO.Type = QrCodeType.STATIC;

        decodeQrCodeResponseDTO = MapperDecodeQrCodeResponseDTO(qrCodeDTO, jws);

        return decodeQrCodeResponseDTO;
    }

    private DecodeQrCodeResponseDTO MapperDecodeQrCodeResponseDTO(QrCodeDTO qrCodeDTO, JWSDTO? jwsDTO = null)
    {
        return new DecodeQrCodeResponseDTO
        {
            Type = qrCodeDTO.Type.ToString(),
            StaticQrCode = qrCodeDTO.Type == QrCodeType.STATIC ? qrCodeDTO.Adapt<StaticQrCodeResponseDTO>() : null,
            ImmediateQrCode = qrCodeDTO.Type == QrCodeType.IMMEDIATE ? ValueTuple.Create(qrCodeDTO, jwsDTO).Adapt<ImmediateQrCodeResponseDTO>() : null,
            DueDateQrCode = qrCodeDTO.Type == QrCodeType.DUEDATE ? ValueTuple.Create(qrCodeDTO, jwsDTO).Adapt<DueDateQrCodeResponseDTO>() : null
        };
    }
}
