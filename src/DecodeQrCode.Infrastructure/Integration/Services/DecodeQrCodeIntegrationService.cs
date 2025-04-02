using DecodeQrCode.Domain.DTOs.Client;
using DecodeQrCode.Domain.DTOs.Client.Response;
using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Enums;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Extensions;
using DecodeQrCode.Domain.Interfaces;
using System.Net;
using System.Text.Json;

namespace DecodeQrCode.Infrastructure.Integration.Services;

public class DecodeQrCodeIntegrationService : IDecodeQrCodeIntegrationService
{
    private readonly IHttpClientService _httpClientService;

    public DecodeQrCodeIntegrationService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<JWSDTO?> DecodeQrCode(QrCodeDTO qrCodeDTO)
    {
        ClientGetRequestDTO clientGetRequestDTO = new()
        {
            Uri = new Uri(qrCodeDTO.MerchantAccountInformation!.URL!.AddSecurityPrefix()),
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

            if (httpClientResponseDTO.Error is not null)
                throw new ServiceException(httpClientResponseDTO.Error.Title ?? httpClientResponseDTO.Error.Message, httpClientResponseDTO.StatusCode);
        }

        string stringJWS = httpClientResponseDTO.Content!;

        JWSDTO? jws = JWSExtensions.ParseJWS(stringJWS);

        return jws;
    }

    public async Task<JKUDTO?> GetJKU(JWSDTO jws)
    {
        ClientGetRequestDTO clientGetRequestDTO = new()
        {
            Uri = new Uri(jws.Header!.JWK!.AddSecurityPrefix()),
            RequestType = RequestType.APPLICATION_JSON,
        };

        ClientResponseDTO httpClientResponseDTO = await _httpClientService.SendGetRequest(clientGetRequestDTO);

        if (httpClientResponseDTO.StatusCode != HttpStatusCode.OK)
        {
            // Salvar erro mongo

            if (httpClientResponseDTO.Error is not null)
                throw new ServiceException(httpClientResponseDTO.Error.Title ?? httpClientResponseDTO.Error.Message, httpClientResponseDTO.StatusCode);
        }

        JKUDTO? jku = JsonSerializer.Deserialize<JKUDTO>(httpClientResponseDTO.Content!);

        return jku;
    }
}
