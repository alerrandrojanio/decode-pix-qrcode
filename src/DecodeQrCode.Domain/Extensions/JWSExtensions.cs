using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.Enums;
using System.Text;
using System.Text.Json;

namespace DecodeQrCode.Domain.Extensions;

public static class JWSExtensions
{
    public static JWSDTO ParseJWS(string uncodedJWS)
    {
        string[] parts = uncodedJWS.Split('.');

        JWSDTO jws = new()
        {
            SerializedHeader = DecodeBase64(parts[(int)JWSParts.HEADER]),
            SerializedPayload = DecodeBase64(parts[(int)JWSParts.PAYLOAD]),
            Signature = parts[(int)JWSParts.SIGNATURE]
        };

        jws.Payload = JsonSerializer.Deserialize<JWSPayloadDTO>(jws.SerializedPayload);
        jws.Header = JsonSerializer.Deserialize<JWSHeaderDTO>(jws.SerializedHeader);

        return jws;
    }

    private static string DecodeBase64(string base64)
    {
        string paddedBase64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=')
                                    .Replace('-', '+')
                                    .Replace('_', '/');

        return Encoding.UTF8.GetString(Convert.FromBase64String(paddedBase64));
    }
}
