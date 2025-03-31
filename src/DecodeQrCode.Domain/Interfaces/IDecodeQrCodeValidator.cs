using DecodeQrCode.Domain.DTOs.QrCode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeValidator
{
    void Validate(QrCodeDTO qrCodeDTO);
}
