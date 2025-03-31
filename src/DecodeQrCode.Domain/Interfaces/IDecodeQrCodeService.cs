using DecodeQrCode.Domain.DTOs.Decode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeService
{
    Task DecodeQrCode(DecodeQrCodeDTO decodeQrCodeDTO);
}
