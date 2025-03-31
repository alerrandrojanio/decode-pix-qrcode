using DecodeQrCode.Domain.DTOs.Client.Response;
using DecodeQrCode.Domain.DTOs.HttpClient;
using DecodeQrCode.Domain.DTOs.HttpClient.Response;
using DecodeQrCode.Domain.Enum;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Extensions;
using System.Net;
using System.Text.Json;

namespace DecodeQrCode.Infrastructure.Integration.Client;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<ClientResponseDTO> SendGetRequest(ClientGetRequestDTO clientGetRequestDTO)
    {
        ConfigureHttpClient(clientGetRequestDTO.RequestType);

        UriBuilder uriBuilder = new(clientGetRequestDTO.Uri!);

        if (clientGetRequestDTO.QueryParams is not null && clientGetRequestDTO.QueryParams.Any())
        {
            string query = string.Join("&", clientGetRequestDTO.QueryParams.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

            uriBuilder.Query = query;
        }

        HttpRequestMessage httpRequest = new(method: HttpMethod.Get, requestUri: uriBuilder.ToString());

        if (clientGetRequestDTO.Headers != null)
        {
            foreach (var header in clientGetRequestDTO.Headers)
                httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);

        string responseContent = await response.Content.ReadAsStringAsync();

        ClientResponseDTO httpClientResponseDTO = new()
        {
            Content = responseContent,
            StatusCode = response.StatusCode
        };

        if (!response.IsSuccessStatusCode)
            httpClientResponseDTO.Error = JsonSerializer.Deserialize<ErrorMessageDTO>(responseContent);

        return httpClientResponseDTO;
    }

    private void ConfigureHttpClient(RequestType requestType)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept", requestType.GetDescription());

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
    }
}
