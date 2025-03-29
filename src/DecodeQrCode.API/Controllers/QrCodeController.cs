﻿using DecodeQrCode.API.Models.Decode;
using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.Interfaces;
using Mapster;
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
    public IActionResult DecodeQrCode(DecodeQrCodeModel decodeQrCodeModel)
    {
        DecodeQrCodeDTO decodeQrCodeDTO = decodeQrCodeModel.Adapt<DecodeQrCodeDTO>();

        _decodeQrCodeService.DecodeQrCode(decodeQrCodeDTO);

        return Ok("Hello World");
    }
}
