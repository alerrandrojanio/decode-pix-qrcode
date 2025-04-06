namespace DecodeQrCode.Domain.DTOs.Decode.Response.Objects;

public class ValueDTO
{
    public string? OriginalValue { get; set; }

    public int? ModalityChange { get; set; }

    public string? Rebate { get; set; }

    public string? Discount { get; set; }

    public string? Interest { get; set; }

    public string? Fine { get; set; }

    public string? Final { get; set; }

    public WithdrawalDTO? Withdrawal { get; set; }

    public ChangeDTO? Change { get; set; }
}
