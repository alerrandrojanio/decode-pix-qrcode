using DecodeQrCode.Domain.Enums;

namespace DecodeQrCode.Domain.DTOs.QrCode;

public class QrCodeDTO
{
    public string? QrCode { get; set; }

    public QrCodeType? Type { get; set; }

    public string? PayloadFormatIndicator { get; set; } 

    public string? PointOfInitiationMethod { get; set; }

    public MerchantAccountInformationDTO? MerchantAccountInformation { get; set; }

    public string? MerchantCategoryCode { get; set; } 

    public string? TransactionCurrency { get; set; }

    public string? TransactionAmount { get; set; }

    public string? CountryCode { get; set; } 

    public string? MerchantName { get; set; }

    public string? MerchantCity { get; set; }

    public AdditionalDataFieldDTO? AdditionalDataField { get; set; }

    public string? CRC16 { get; set; }
}
