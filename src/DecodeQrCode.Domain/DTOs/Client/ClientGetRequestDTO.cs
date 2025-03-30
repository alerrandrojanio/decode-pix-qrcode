using DecodeQrCode.Domain.DTOs.HttpClient.Base;
using DecodeQrCode.Domain.Enum;

namespace DecodeQrCode.Domain.DTOs.HttpClient;

public class ClientGetRequestDTO : ClientRequestDTO
{
    public Dictionary<string, string>? Headers { get; set; }

    public Dictionary<string, string>? QueryParams { get; set; }

    public string? Content { get; set; }

    public RequestType RequestType { get; set; }
}
