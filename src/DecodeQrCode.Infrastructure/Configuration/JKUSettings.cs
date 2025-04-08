namespace DecodeQrCode.Infrastructure.Configuration;

public class JKUSettings
{
    public string MandatoryProperties { get; set; } = string.Empty;

    public string RSAMandatoryProperties { get; set; } = string.Empty;

    public string ECMandatoryProperties { get; set; } = string.Empty;

    public string CertficateChainProperties { get; set; } = string.Empty;
}
