using DecodeQrCode.Domain.DTOs.Decode.Response.Objects;

namespace DecodeQrCode.Domain.DTOs.Decode.Response;

public class DueDateQrCodeResponseDTO
{

    public string? CreationDate { get; set; }

    public string? PresentationDate { get; set; }

    public string? DueDate { get; set; }

    public int? ValidityAfterExpiration { get; set; }

    public ValueDTO? Value { get; set; }

    public DebtorDTO? Debtor { get; set; }

    public RecipientDTO? Recipient { get; set; }

    public string? PayloadFormatIndicator { get; set; }

    public string? TransactionCurrency { get; set; }

    public string? GUI { get; set; }

    public string? TxId { get; set; }

    public string? Key { get; set; }

    public string? KeyType { get; set; }

    public string? PayerSolicitation { get; set; }

    public string? CRC16 { get; set; }

    public string? Status { get; set; }

    public List<AdditionalInformationResponseDTO>? AdditionalInformation { get; set; }
}
