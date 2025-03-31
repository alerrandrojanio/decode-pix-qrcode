using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.DTOs.Decode.Response;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeService
{
    Task<DecodeQrCodeResponseDTO?> DecodeQrCode(DecodeQrCodeDTO decodeQrCodeDTO);
}
