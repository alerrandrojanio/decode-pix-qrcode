﻿using DecodeQrCode.Domain.Enum;

namespace DecodeQrCode.Domain.DTOs.Decode.Response;

public class DecodeQrCodeResponseDTO
{
    public QrCodeType Type { get; set; }

    public StaticQrCodeResponseDTO? StaticQrCode { get; set; }

    public ImmediateQrCodeResponseDTO? ImmediateQrCode { get; set; }

    public DueDateQrCodeResponseDTO? DueDateQrCode { get; set; }
}
