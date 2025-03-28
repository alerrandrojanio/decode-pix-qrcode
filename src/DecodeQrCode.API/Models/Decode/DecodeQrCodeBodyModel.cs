using System.Text.Json.Serialization;

namespace DecodeQrCode.API.Models.Decode;

public class DecodeQrCodeBodyModel
{
    [JsonPropertyName("qrcode")]
    public string QrCode { get; set; } = string.Empty;
}
