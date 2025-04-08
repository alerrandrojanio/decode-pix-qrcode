using System.Security.Authentication;

namespace DecodeQrCode.Domain.DTOs.Certificate;

public class ServerCertificateDTO
{
    public string? Issuer { get; set; }

    public string? Subject { get; set; }

    public string? CommonName { get; set; }

    public List<string> AlternativeNames { get; set; } = new();

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidUntil { get; set; }

    public string? Thumbprint { get; set; }

    public SslProtocols? Protocol { get; set; }
}
