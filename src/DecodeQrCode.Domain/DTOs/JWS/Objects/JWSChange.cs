using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSChange
{
    [JsonPropertyName("valor")]
    public string? Value { get; set; }

    [JsonPropertyName("modalidadeAlteracao")]
    public int? AlterModality { get; set; }

    [JsonPropertyName("prestadorDoServicoDeSaque")]
    public string? WithdrawalServiceProvider { get; set; }

    [JsonPropertyName("modalidadeAgente")]
    public string? AgentModality { get; set; }
}
