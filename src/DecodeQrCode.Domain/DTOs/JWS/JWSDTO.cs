namespace DecodeQrCode.Domain.DTOs.JWS;

public class JWSDTO
{
    public JWSHeaderDTO? Header { get; set; }

    public JWSPayloadDTO? Payload { get; set; }

    public string? SerializedHeader { get; set; }

    public string? SerializedPayload { get; set; }

    public string? Signature { get; set; }
}
