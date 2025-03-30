using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Enum;
using DecodeQrCode.Domain.Interfaces;

namespace DecodeQrCode.Application.Services;

public class DecodeService : IDecodeService
{
    public QrCodeDTO DecodeQrCode(string qrCode)
    {
        Dictionary<string, QrCodeField> QrCodeFields = new()
        {
            { "00", QrCodeField.PAYLOAD_FORMAT_INDICATOR },
            { "01", QrCodeField.POINT_OF_INITIATION_METHOD },
            { "26", QrCodeField.MERCHANT_ACCOUNT_INFORMATION },
            { "52", QrCodeField.MERCHANT_CATEGORY_CODE },
            { "53", QrCodeField.TRANSACTION_CURRENCY },
            { "58", QrCodeField.COUNTRY_CODE },
            { "59", QrCodeField.MERCHANT_NAME },
            { "60", QrCodeField.MERCHANT_CITY },
            { "62", QrCodeField.ADDITIONAL_DATA_FIELD_TEMPLATE },
            { "63", QrCodeField.CRC16 },
        };

        int index = 0;
        string value = string.Empty;
        string id = string.Empty;

        QrCodeDTO qrCodeDTO = new();

        while (index < qrCode.Length)
        {
            (id, value, index) = ExtractField(qrCode, index);

            if (QrCodeFields.TryGetValue(id, out QrCodeField field))
            {
                switch (field)
                {
                    case QrCodeField.PAYLOAD_FORMAT_INDICATOR:
                        qrCodeDTO.PayloadFormatIndicator = value;
                        break;

                    case QrCodeField.POINT_OF_INITIATION_METHOD:
                        qrCodeDTO.PointOfInitiationMethod = value;
                        break;

                    case QrCodeField.MERCHANT_ACCOUNT_INFORMATION:
                        qrCodeDTO.MerchantAccountInformation = DecodeMerchantAccountInformation(value);
                        break;

                    case QrCodeField.MERCHANT_CATEGORY_CODE:
                        qrCodeDTO.MerchantCategoryCode = value;
                        break;

                    case QrCodeField.TRANSACTION_CURRENCY:
                        qrCodeDTO.TransactionCurrency = value;
                        break;

                    case QrCodeField.COUNTRY_CODE:
                        qrCodeDTO.CountryCode = value;
                        break;

                    case QrCodeField.MERCHANT_NAME:
                        qrCodeDTO.MerchantName = value;
                        break;

                    case QrCodeField.MERCHANT_CITY:
                        qrCodeDTO.MerchantCity = value;
                        break;

                    case QrCodeField.ADDITIONAL_DATA_FIELD_TEMPLATE:
                        qrCodeDTO.AdditionalDataField = DecodeAdditionalData(value);
                        break;

                    case QrCodeField.CRC16:
                        qrCodeDTO.CRC16 = value;
                        break;
                }
            }
        }

        return qrCodeDTO;
    }

    private static MerchantAccountInformationDTO DecodeMerchantAccountInformation(string merchantInfo)
    {
        Dictionary<string, QrCodeField> MerchantAccountInformationFields = new()
        {
            { "00", QrCodeField.GUI },
            { "01", QrCodeField.KEY },
            { "02", QrCodeField.ADDITIONAL_INFORMATION },
            { "03", QrCodeField.FSS },
            { "25", QrCodeField.URL },
        };

        MerchantAccountInformationDTO merchantAccountInformationDTO = new();
        int index = 0;

        while (index < merchantInfo.Length)
        {
            (string id, string value, int newIndex) = ExtractField(merchantInfo, index);
            index = newIndex;

            if (MerchantAccountInformationFields.TryGetValue(id, out QrCodeField field))
            {
                switch (field)
                {
                    case QrCodeField.GUI:
                        merchantAccountInformationDTO.GUI = value;
                        break;
                    
                    case QrCodeField.KEY:
                        merchantAccountInformationDTO.Key = value;
                        break;
                    
                    case QrCodeField.ADDITIONAL_INFORMATION:
                        merchantAccountInformationDTO.AdditionalInformation = value;
                        break;
                    
                    case QrCodeField.FSS:
                        merchantAccountInformationDTO.FSS = value;
                        break;
                    
                    case QrCodeField.URL:
                        merchantAccountInformationDTO.URL = value;
                        break;
                }
            }
        }

        return merchantAccountInformationDTO;
    }

    private static AdditionalDataFieldDTO DecodeAdditionalData(string additionalData)
    {
        Dictionary<string, QrCodeField> MerchantAdditionalDataFields = new()
        {
            { "05", QrCodeField.TXID }
        };

        AdditionalDataFieldDTO additionalDataFieldDTO = new();
        int index = 0;

        while (index < additionalData.Length)
        {
            (string id, string value, int newIndex) = ExtractField(additionalData, index);
            index = newIndex;

            if (MerchantAdditionalDataFields.TryGetValue(id, out QrCodeField field))
            {
                switch (field)
                {
                    case QrCodeField.TXID:
                        additionalDataFieldDTO.TxId = value;
                        break;
                }
            }
        }

        return additionalDataFieldDTO;
    }

    private static (string id, string value, int index) ExtractField(string qrCode, int index)
    {
        if (index + 4 > qrCode.Length)
            throw new Exception();

        string id = qrCode.Substring(index, 2); 
        
        int length = int.Parse(qrCode.Substring(index + 2, 2));

        if (index + 4 + length > qrCode.Length) 
            throw new Exception();

        string value = qrCode.Substring(index + 4, length);
        index += 4 + length;

        return (id, value, index);
    }
}
