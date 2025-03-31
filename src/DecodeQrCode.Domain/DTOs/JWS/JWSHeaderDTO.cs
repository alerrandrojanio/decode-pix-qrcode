using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS;

public class JWSHeaderDTO
{
    [JsonPropertyName("alg")]
    public string? Algorithm { get; set; }

    [JsonPropertyName("x5t")]
    public string? X509CertificateThumbprint { get; set; }

    [JsonPropertyName("jku")]
    public string? JWK { get; set; }

    [JsonPropertyName("kid")]
    public string? KeyId { get; set; }

    [JsonPropertyName("typ")]
    public string? KeyType { get; set; }

    [JsonPropertyName("key_ops")]
    public string? KeyOperations { get; set; }

    [JsonPropertyName("x5c")]
    public string? X509CertificateChain { get; set; }
}
