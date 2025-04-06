using DecodeQrCode.Domain.DTOs.Client;
using DecodeQrCode.Domain.DTOs.Client.Response;
using DecodeQrCode.Domain.Enums;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Extensions;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Integration.Messages.Error;
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

        ClientResponseDTO clientResponseDTO = new()
        {
            Content = responseContent,
            StatusCode = response.StatusCode
        };

        return clientResponseDTO;
    }

    public void ProcessClientError(ClientResponseDTO clientResponseDTO)
    {
        ErrorMessage? errorMessage = JsonSerializer.Deserialize<ErrorMessage>(clientResponseDTO.Content!);

        if (errorMessage is not null)
            throw new ServiceException(errorMessage.Title ?? errorMessage.Message ?? errorMessage.MessageError, clientResponseDTO.StatusCode);
    }
    
    private void ConfigureHttpClient(RequestType requestType)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept", requestType.GetDescription());

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
    }
}
