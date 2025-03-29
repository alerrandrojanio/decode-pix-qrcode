using DecodeQrCode.API.Models.Decode;
using DecodeQrCode.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DecodeQrCode.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/qrcode")]
public class QrCodeController : ControllerBase
{
    private readonly IDecodeQrCodeService _decodeQrCodeService;

    public QrCodeController(IDecodeQrCodeService decodeQrCodeService)
    {
        _decodeQrCodeService = decodeQrCodeService;
    }

    [HttpPost("decode")]
    public IActionResult Decode(DecodeQrCodeModel decodeQrCodeModel)
    {
        return Ok("Hello World");
    }
}
