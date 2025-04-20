namespace DecodeQrCode.Infrastructure.Configuration;

public class QrCodeSettings
{
    public bool AcceptsHomologationCode { get; set; }

    public string HomologationSufix { get; set; } = string.Empty;

    public int UrlMinumumBitSize { get; set; }

    public string HostHomologationPrefix { get; set; } = string.Empty;
}
