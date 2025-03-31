namespace DecodeQrCode.Domain.DTOs.JWS;

public class JWS
{
    public JWSHeader? Header { get; set; }

    public JWSPayload? Payload { get; set; }

    public string? SerializedHeader { get; set; }

    public string? SerializedPayload { get; set; }

    public string? Signature { get; set; }
}
