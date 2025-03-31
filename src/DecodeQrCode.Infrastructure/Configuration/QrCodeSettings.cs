namespace DecodeQrCode.Infrastructure.Configuration;

public class QrCodeSettings
{
    public bool AcceptsHomologationCode { get; set; }

    public string HomologationPrefix { get; set; } = string.Empty;

    public int UriMinumumBitLength { get; set; }
}
