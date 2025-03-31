using DecodeQrCode.Domain.DTOs.QrCode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeIntegrationService
{
    Task DecodeQrCode(QrCodeDTO qrCodeDTO);
}
