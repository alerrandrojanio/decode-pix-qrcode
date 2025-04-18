using DecodeQrCode.Domain.DTOs.Entities.Base;

namespace DecodeQrCode.Domain.DTOs.Entities;

public class ErrorLog : MongoBaseEntity
{
    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? Source { get; set; }

    public string? InnerException { get; set; }
}
