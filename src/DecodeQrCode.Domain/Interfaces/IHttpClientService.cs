using DecodeQrCode.Domain.DTOs.Client;
using DecodeQrCode.Domain.DTOs.Client.Response;

namespace DecodeQrCode.Domain.Interfaces;

public interface IHttpClientService
{
    Task<ClientResponseDTO> SendGetRequest(ClientGetRequestDTO clientGetRequestDTO);
}
