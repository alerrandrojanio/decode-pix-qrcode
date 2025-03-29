namespace DecodeQrCode.Domain.DTOs.Decode;

public class DecodeQrCodeDTO
{
    public bool IsHomologation { get; set; }

    public string QrCode { get; set; } = string.Empty;
}
