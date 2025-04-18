using DecodeQrCode.Domain.DTOs.Logging;

namespace DecodeQrCode.Domain.Interfaces;

public interface IDbLogger
{
    Task SaveErrorLog(ErrorLogDTO errorLogDTO);
}
