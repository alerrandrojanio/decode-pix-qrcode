using DecodeQrCode.Domain.DTOs.Client.Response;
using System.Net;

namespace DecodeQrCode.Domain.DTOs.HttpClient.Response;

public class ClientResponseDTO
{
    public string? Content { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public ErrorMessageDTO? Error { get; set; }
}
