using DecodeQrCode.API.Models.Decode;
using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.DTOs.Decode.Response;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.DTOs.QrCode;
using Mapster;

namespace DecodeQrCode.API.Mapping;

public static class MappingConfiguration
{
    public static void RegisterMappings()
    {
        #region DecodeQrCode
        TypeAdapterConfig<DecodeQrCodeModel, DecodeQrCodeDTO>.NewConfig()
           .Map(dest => dest.QrCode, src => src.Body!.QrCode);

        #region StaticQrCode
        TypeAdapterConfig<QrCodeDTO, StaticQrCodeResponseDTO>.NewConfig();
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
