using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSDebtorDTO
{
    [JsonPropertyName("cpf")]
    public string? CPF { get; set; }

    [JsonPropertyName("cnpj")]
    public string? CNPJ { get; set; }

    [JsonPropertyName("nome")]
    public string? Name { get; set; }
}
