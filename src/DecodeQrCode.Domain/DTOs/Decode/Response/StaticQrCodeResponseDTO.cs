namespace DecodeQrCode.Domain.DTOs.Decode.Response;

public class StaticQrCodeResponseDTO
{
    public string? PayloadFormatIndicator { get; set; }

    public string? PointOfInitiationMethod { get; set; }

    public string? MerchantName { get; set; }

    public string? MerchantCity { get; set; }

    public string? MerchantCategoryCode { get; set; }

    public string? CountryCode { get; set; }

    public string? PixKey { get; set; }

    public string? KeyType { get; set; }

    public decimal? TransactionAmount { get; set; }

    public string? TransactionCurrency { get; set; }

    public string? FSS { get; set; }

    public string? GUI { get; set; }

    public string? TxId { get; set; } 

    public string? AdditionalData { get; set; }

    public string? CRC16 { get; set; }
}
