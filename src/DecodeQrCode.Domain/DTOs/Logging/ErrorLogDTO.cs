namespace DecodeQrCode.Domain.DTOs.Logging;

public class ErrorLogDTO
{
    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? Source { get; set; }

    public string? InnerException { get; set; }
}
