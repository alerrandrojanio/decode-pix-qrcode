using System.Text.Json.Serialization;

namespace DecodeQrCode.Infrastructure.Integration.Messages.Error;

public class ErrorMessage
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("mensagemErro")]
    public string? MessageError { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("status")]
    public int? Status { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }
}
