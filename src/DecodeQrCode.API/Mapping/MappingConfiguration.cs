using DecodeQrCode.API.Models.Decode;
using DecodeQrCode.Domain.DTOs.Decode;
using Mapster;

namespace DecodeQrCode.API.Mapping;

public static class MappingConfiguration
{
    public static void RegisterMappings()
    {
        #region DecodeQrCode
        TypeAdapterConfig<DecodeQrCodeModel, DecodeQrCodeDTO>.NewConfig()
           .Map(dest => dest.QrCode, src => src.Body!.QrCode);
        #endregion DecodeQrCode
    }
}
