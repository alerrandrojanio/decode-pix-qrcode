using DecodeQrCode.Domain.DTOs.QrCode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeValidator
{
    Task Validate(QrCodeDTO qrCodeDTO);
}
