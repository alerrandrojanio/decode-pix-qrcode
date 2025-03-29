using DecodeQrCode.Domain.DTOs.QrCode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeService
{
    QrCodeDTO DecodeQrCode(string qrCode);
}
