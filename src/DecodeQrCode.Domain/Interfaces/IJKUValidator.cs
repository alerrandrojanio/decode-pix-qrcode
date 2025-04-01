using DecodeQrCode.Domain.DTOs.JKU;

namespace DecodeQrCode.Domain.Interfaces;

public interface IJKUValidator
{
    public void Validate(JKUDTO jku);
}
