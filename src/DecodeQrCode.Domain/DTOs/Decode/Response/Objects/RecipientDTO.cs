namespace DecodeQrCode.Domain.DTOs.Decode.Response.Objects;

public class RecipientDTO
{
    public string? CPF { get; set; }

    public string? CNPJ { get; set; }

    public string? Name { get; set; }

    public string? FantasyName { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }
}
