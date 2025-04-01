using DecodeQrCode.API.Models.Decode;
using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.DTOs.Decode.Response;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Extensions;
using Mapster;
using System.Globalization;

namespace DecodeQrCode.API.Mapping;

public static class MappingConfiguration
{
    public static void RegisterMappings()
    {
        #region DecodeQrCode
        TypeAdapterConfig<DecodeQrCodeModel, DecodeQrCodeDTO>.NewConfig()
           .Map(dest => dest.QrCode, src => src.Body!.QrCode);

        #region StaticQrCode
        TypeAdapterConfig<QrCodeDTO, StaticQrCodeResponseDTO>.NewConfig()
            .Map(dest => dest.MerchantCategoryCode, src => src.MerchantCategoryCode ?? "0000")
            .Map(dest => dest.PixKey, src => src.MerchantAccountInformation!.Key)
            .Map(dest => dest.KeyType, src => src.MerchantAccountInformation!.Key!.GetPixKeyType().ToString())
            .Map(dest => dest.FSS, src => src.MerchantAccountInformation!.FSS)
            .Map(dest => dest.GUI, src => src.MerchantAccountInformation!.GUI)
            .Map(dest => dest.AdditionalData, src => src.MerchantAccountInformation!.AdditionalInformation)
            .Map(dest => dest.TxId, src => src.AdditionalDataField!.TxId)
            .Map(dest => dest.TransactionAmount, src => Convert.ToDecimal(src.TransactionAmount, CultureInfo.InvariantCulture));
        #endregion StaticQrCode

        #region ImmediateQrCode
        TypeAdapterConfig<(QrCodeDTO qrCodeDTO, JWSDTO jwsDTO), ImmediateQrCodeResponseDTO>.NewConfig();
        #endregion ImmediateQrCode

        #region DueDateQrCode
        TypeAdapterConfig<(QrCodeDTO qrCodeDTO, JWSDTO jwsDTO), DueDateQrCodeResponseDTO>.NewConfig();
        #endregion DueDateQrCode

        #endregion DecodeQrCode
    }
}
