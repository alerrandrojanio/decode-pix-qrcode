using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSRecipientDTO
{
    [JsonPropertyName("cpf")]
    public string? CPF { get; set; }

    [JsonPropertyName("cnpj")]
    public string? CNPJ { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("nomeFantasia")]
    public string? FantasyName { get; set; }

    [JsonPropertyName("logradouro")]
    public string? Street { get; set; }

    [JsonPropertyName("cidade")]
    public string? City { get; set; }

    [JsonPropertyName("uf")]
    public string? State { get; set; }

    [JsonPropertyName("cep")]
    public string? ZipCode { get; set; }
}
