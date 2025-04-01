using DecodeQrCode.Domain.DTOs.JKU.Objects;
using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JKU;

public class JKUDTO
{
    [JsonPropertyName("keys")]
    public List<KeyDTO>? Keys { get; set; }
}
