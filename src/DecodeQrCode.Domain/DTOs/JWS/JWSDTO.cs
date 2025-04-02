namespace DecodeQrCode.Domain.DTOs.JWS;

public class JWSDTO
{
    public JWSHeaderDTO? Header { get; set; }

    public JWSPayloadDTO? Payload { get; set; }

    public string? JWS { get; set; }
}
