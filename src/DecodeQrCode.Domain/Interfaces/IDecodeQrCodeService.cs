using DecodeQrCode.Domain.DTOs.Decode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeService
{
    void DecodeQrCode(DecodeQrCodeDTO decodeQrCodeDTO);
}
