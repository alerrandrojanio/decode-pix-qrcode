using DecodeQrCode.Domain.DTOs.HttpClient;
using DecodeQrCode.Domain.DTOs.HttpClient.Response;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Enum;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Extensions;
using System.Net;

namespace DecodeQrCode.Infrastructure.Integration.Services;

public class DecodeQrCodeIntegrationService : IDecodeQrCodeIntegrationService
{
    private readonly IHttpClientService _httpClientService;

    public DecodeQrCodeIntegrationService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async void DecodeQrCode(QrCodeDTO qrCodeDTO)
    {
        ClientGetRequestDTO clientGetRequestDTO = new()
        {
            Uri = new Uri(qrCodeDTO.MerchantAccountInformation!.URL.AddSecurityPrefix()),
            RequestType = RequestType.APPLICATION_JSON,
        };

        ClientResponseDTO httpClientResponseDTO = await _httpClientService.SendGetRequest(clientGetRequestDTO);

        if (httpClientResponseDTO.StatusCode == HttpStatusCode.NotAcceptable)
        {
            clientGetRequestDTO.RequestType = RequestType.APPLICATION_JOSE;

            httpClientResponseDTO = await _httpClientService.SendGetRequest(clientGetRequestDTO);
        }

        if (httpClientResponseDTO.StatusCode != HttpStatusCode.OK)
        {
            // Salvar erro mongo
            throw new Exception();
        }

    }
}
