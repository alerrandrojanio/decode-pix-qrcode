﻿namespace DecodeQrCode.Domain.DTOs.Decode.Response.Objects;

public class WithdrawalDTO
{
    public string? Value { get; set; }

    public int? AlterModality { get; set; }

    public string? WithdrawalServiceProvider { get; set; }

    public string? AgentModality { get; set; }
}
