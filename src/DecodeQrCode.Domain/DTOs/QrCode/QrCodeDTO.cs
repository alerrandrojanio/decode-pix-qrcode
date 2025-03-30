using DecodeQrCode.Domain.Enum;

namespace DecodeQrCode.Domain.DTOs.QrCode;

public class QrCodeDTO
{
    public QrCodeType? Type { get; set; }

    public string PayloadFormatIndicator { get; set; } = string.Empty;

    public string PointOfInitiationMethod { get; set; } = string.Empty;

    public MerchantAccountInformationDTO? MerchantAccountInformation { get; set; }

    public string MerchantCategoryCode { get; set; } = string.Empty;

    public string TransactionCurrency { get; set; } = string.Empty;

    public string CountryCode { get; set; } = string.Empty;

    public string MerchantName { get; set; } = string.Empty;

    public string MerchantCity { get; set; } = string.Empty;

    public AdditionalDataFieldDTO? AdditionalDataField { get; set; }

    public string CRC16 { get; set; } = string.Empty;
}
