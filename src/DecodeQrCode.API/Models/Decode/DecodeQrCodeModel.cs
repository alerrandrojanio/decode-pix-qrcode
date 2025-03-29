using Microsoft.AspNetCore.Mvc;

namespace DecodeQrCode.API.Models.Decode;

public class DecodeQrCodeModel
{
    [FromBody]
    public DecodeQrCodeBodyModel? Body { get; set; }
}
