using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSRemoval
{
    [JsonPropertyName("saque")]
    public JWSWithdrawal? Withdrawal { get; set; }

    [JsonPropertyName("troco")]
    public JWSChange? Change { get; set; }
}
