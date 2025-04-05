using DecodeQrCode.Domain.DTOs.JWS.Objects;
using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS;

public class JWSPayloadDTO
{
    [JsonPropertyName("revisao")]
    public int? Revision { get; set; }

    [JsonPropertyName("calendario")]
    public JWSCalendarDTO? Calendar { get; set; }

    [JsonPropertyName("devedor")]
    public JWSDebtorDTO? Debtor { get; set; }

    [JsonPropertyName("recebedor")]
    public JWSRecipientDTO? Recipient { get; set; }

    [JsonPropertyName("valor")]
    public JWSValueDTO? Value { get; set; }

    [JsonPropertyName("chave")]
    public string? Key { get; set; }

    [JsonPropertyName("txid")]
    public string? TxId { get; set; }

    [JsonPropertyName("solicitacaoPagador")]
    public string? PayerSolicitation { get; set; }

    [JsonPropertyName("infoAdicionais")]
    public List<JWSAdditionalInformationDTO>? AdditionalInformation { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
