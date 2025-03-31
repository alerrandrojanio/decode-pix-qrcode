using DecodeQrCode.Domain.DTOs.Client.Base;
using DecodeQrCode.Domain.Enums;

namespace DecodeQrCode.Domain.DTOs.Client;

public class ClientGetRequestDTO : ClientRequestDTO
{
    public Dictionary<string, string>? Headers { get; set; }

    public Dictionary<string, string>? QueryParams { get; set; }

    public string? Content { get; set; }

    public RequestType RequestType { get; set; }
}
