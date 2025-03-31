using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSAdditionalInformationDTO
{
    [JsonPropertyName("nome")]
    public string? Name { get; set; }

    [JsonPropertyName("valor")]
    public string? Value { get; set; }
}
