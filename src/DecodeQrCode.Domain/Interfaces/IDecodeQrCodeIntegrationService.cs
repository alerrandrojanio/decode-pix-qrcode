﻿using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.DTOs.QrCode;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDecodeQrCodeIntegrationService
{
    Task<JWSDTO?> DecodeQrCode(QrCodeDTO qrCodeDTO);

    Task<JKUDTO?> GetJKU(JWSDTO jws);
}
