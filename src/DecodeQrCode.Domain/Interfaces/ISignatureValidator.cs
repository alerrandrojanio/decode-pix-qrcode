using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.DTOs.JWS;

namespace DecodeQrCode.Domain.Interfaces;

public interface ISignatureValidator
{
    bool Validate(JWSDTO jws, JKUDTO jku);
}
