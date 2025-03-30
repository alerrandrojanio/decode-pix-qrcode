using DecodeQrCode.Domain.DTOs.QrCode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeIntegrationService
{
    void DecodeQrCode(QrCodeDTO qrCodeDTO);
}
