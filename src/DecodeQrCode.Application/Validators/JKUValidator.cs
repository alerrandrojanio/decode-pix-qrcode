using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace DecodeQrCode.Application.Validators;

public class JKUValidator : IJKUValidator
{
    private readonly JKUSettings _jkuSettings;

    public JKUValidator(IOptions<JKUSettings> jkuSettings)
    {
        _jkuSettings = jkuSettings.Value;
    }

    public void Validate(JKUDTO jku)
    {
        List<string> mandatoryProperties = _jkuSettings.MandatoryProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
        List<string> rsaMandatoryProperties = _jkuSettings.RSAMandatoryProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
        List<string> ecMandatoryProperties = _jkuSettings.ECMandatoryProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
        List<string> certficateChainProperties = _jkuSettings.CertficateChainProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
