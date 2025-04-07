using System.Security.Authentication;

namespace DecodeQrCode.Domain.DTOs.Certificate;

public class ServerCertificateDTO
{
    public string Issuer { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string CommonName { get; set; } = string.Empty;

    public List<string> AlternativeNames { get; set; } = new();

    public DateTime ValidFrom { get; set; }

    public DateTime ValidUntil { get; set; }

    public string Thumbprint { get; set; } = string.Empty;

    public SslProtocols? Protocol { get; set; }
}
