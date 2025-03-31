using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSDebtor
{
    [JsonPropertyName("cpf")]
    public string? CPF { get; set; }

    [JsonPropertyName("cnpj")]
    public string? CNPJ { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
