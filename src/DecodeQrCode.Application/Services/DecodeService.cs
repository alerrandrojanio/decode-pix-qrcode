using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Enum;
using DecodeQrCode.Domain.Interfaces;

namespace DecodeQrCode.Application.Services;

public class DecodeService : IDecodeService
{
    public static QrCodeDTO Decode(string qrCode)
    {
        Dictionary<string, QrCodeField> QrCodeFields = new()
        {
            { "00", QrCodeField.PAYLOAD_FORMAT_INDICATOR },
            { "26", QrCodeField.MERCHANT_ACCOUNT_INFORMATION },
            { "52", QrCodeField.MERCHANT_CATEGORY_CODE },
            { "53", QrCodeField.TRANSACTION_CURRENCY },
            { "58", QrCodeField.COUNTRY_CODE },
            { "59", QrCodeField.MERCHANT_NAME },
            { "60", QrCodeField.MERCHANT_CITY },
            { "62", QrCodeField.ADDITIONAL_DATA_FIELD_TEMPLATE },
            { "63", QrCodeField.CRC16 },
        };

        return new QrCodeDTO();
    }

    private static void DecodeMerchantAccountInformation()
    {
        Dictionary<string, QrCodeField> MerchantAccountInformationFields = new()
        {
            { "00", QrCodeField.GUI },
            { "01", QrCodeField.KEY },
            { "02", QrCodeField.ADDITIONAL_INFORMATION },
            { "03", QrCodeField.FSS },
            { "25", QrCodeField.URL },
        };
    }

    private static void DecodeAdditionalData()
    {
        Dictionary<string, QrCodeField> MerchantAdditionalDataFields = new()
        {
            { "05", QrCodeField.TXID }
        };
    }
}
