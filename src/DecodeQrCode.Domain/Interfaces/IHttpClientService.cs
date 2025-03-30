using DecodeQrCode.Domain.DTOs.HttpClient;
using DecodeQrCode.Domain.DTOs.HttpClient.Response;

namespace DecodeQrCode.Domain.Interfaces;

public interface IHttpClientService
{
    Task<ClientResponseDTO> SendGetRequest(ClientGetRequestDTO clientGetRequestDTO);
}
