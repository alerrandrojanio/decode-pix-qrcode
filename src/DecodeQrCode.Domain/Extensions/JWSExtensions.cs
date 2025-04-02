using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.Enums;
using System.Text.Json;

namespace DecodeQrCode.Domain.Extensions;

public static class JWSExtensions
{
    public static JWSDTO ParseJWS(string uncodedJWS)
    {
        string[] parts = uncodedJWS.Split('.');

        if (parts.Length != 3)
            throw new Exception("Invalid JWS");

        string header = Base64Extensions.DecodeBase64ToString(parts[(int)JWSParts.HEADER]);
        string payload = Base64Extensions.DecodeBase64ToString(parts[(int)JWSParts.PAYLOAD]);

        JWSDTO jws = new()
        {
            Payload = JsonSerializer.Deserialize<JWSPayloadDTO>(header),
            Header = JsonSerializer.Deserialize<JWSHeaderDTO>(payload),
            JWS = uncodedJWS
        };

        return jws;
    }

    
}
