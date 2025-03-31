using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSValueDTO
{
    [JsonPropertyName("original")]
    public string? OriginalValue { get; set; }

    [JsonPropertyName("modalidadeAlteracao")]
    public int? ModalityChange { get; set; }

    [JsonPropertyName("retirada")]
    public JWSRemovalDTO? Removal { get; set; }

    [JsonPropertyName("abatimento")]
    public string? Rebate { get; set; }

    [JsonPropertyName("desconto")]
    public string? Discount { get; set; }

    [JsonPropertyName("juros")]
    public string? Interest { get; set; }

    [JsonPropertyName("multa")]
    public string? Fine { get; set; }

    [JsonPropertyName("final")]
    public string? Final { get; set; }
}
