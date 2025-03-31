﻿using System.Text.Json.Serialization;

namespace DecodeQrCode.Domain.DTOs.JWS.Objects;

public class JWSWithdrawal
{
    [JsonPropertyName("valor")]
    public string? Value { get; set; }

    [JsonPropertyName("modalidade")]
    public int? Modality { get; set; }

    [JsonPropertyName("prestadorDoServicoDeSaque")]
    public string? WithdrawalServiceProvider { get; set; }

    [JsonPropertyName("modalidadeAgente")]
    public string? AgentModality { get; set; }
}
