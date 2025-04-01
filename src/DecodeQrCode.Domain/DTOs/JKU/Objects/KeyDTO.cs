using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JKU.Objects;

public class KeyDTO
{
    [JsonPropertyName("kty")]
    public string? KeyType { get; set; }

    [JsonPropertyName("n")]
    public string? Modulus { get; set; }

    [JsonPropertyName("e")]
    public string? Exponent { get; set; }

    [JsonPropertyName("crv")]
    public string? Curve { get; set; } 

    [JsonPropertyName("x")]
    public string? XCoordinate { get; set; }

    [JsonPropertyName("y")]
    public string? YCoordinate { get; set; } 

    [JsonPropertyName("x5c")]
    public List<string>? X509CertificateChain { get; set; }

    [JsonPropertyName("x5t")]
    public string? X509CertificateThumbprint { get; set; }

    [JsonPropertyName("x5t#256")]
    public string? X509CertificateThumbprintSha256 { get; set; }

    [JsonPropertyName("key_ops")]
    public List<string>? KeyOperations { get; set; }

    [JsonPropertyName("kid")]
    public string? KeyId { get; set; }
}