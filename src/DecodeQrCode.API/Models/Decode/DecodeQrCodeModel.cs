using Microsoft.AspNetCore.Mvc;

namespace DecodeQrCode.API.Models.Decode;

public class DecodeQrCodeModel
{
    [FromHeader]
    public bool IsHomologation { get; set; }

    [FromBody]
    public DecodeQrCodeBodyModel? Body { get; set; }
}
