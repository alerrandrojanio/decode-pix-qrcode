using DecodeQrCode.Domain.DTOs.JWS.Objects;
using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS;

public class JWSPayload
{
    [JsonPropertyName("revisao")]
    public int? Revision { get; set; }

    [JsonPropertyName("calendario")]
    public JWSCalendar? Calendar { get; set; }

    [JsonPropertyName("devedor")]
    public JWSDebtor? Debtor { get; set; }

    [JsonPropertyName("recebedor")]
    public JWSRecipient? Recipient { get; set; }

    [JsonPropertyName("valor")]
    public JWSValue? Value { get; set; }

    [JsonPropertyName("chave")]
    public string? Key { get; set; }

    [JsonPropertyName("txid")]
    public string? TxId { get; set; }

    [JsonPropertyName("solicitacaoPagador")]
    public string? PayerRequest { get; set; }

    [JsonPropertyName("infoAdicionais")]
    public List<JWSAdditionalInformation>? AdditionalInformation { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
