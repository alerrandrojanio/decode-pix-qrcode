using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSRemovalDTO
{
    [JsonPropertyName("saque")]
    public JWSWithdrawalDTO? Withdrawal { get; set; }

    [JsonPropertyName("troco")]
    public JWSChangeDTO? Change { get; set; }
}
