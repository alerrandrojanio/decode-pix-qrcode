using DecodeQrCode.Domain.DTOs.Certificate;

namespace DecodeQrCode.Domain.Interfaces;

public interface ICertificateIntegrationService
{
    Task<ServerCertificateDTO?> GetServerCertificate(string url);
}
