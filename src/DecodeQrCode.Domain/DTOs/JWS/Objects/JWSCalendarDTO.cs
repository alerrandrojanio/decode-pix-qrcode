﻿using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSCalendarDTO
{
    [JsonPropertyName("criacao")]
    public string? CreationDate { get; set; }

    [JsonPropertyName("apresentacao")]
    public string? PresentationDate { get; set; }

    [JsonPropertyName("expiracao")]
    public int? Expiration { get; set; }

    [JsonPropertyName("dataDeVencimento")]
    public string? DueDate { get; set; }

    [JsonPropertyName("validadeAposVencimento")]
    public int? ValidityAfterExpiration { get; set; }
}
