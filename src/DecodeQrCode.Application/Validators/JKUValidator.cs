using DecodeQrCode.Application.Resources;
using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.DTOs.JKU.Objects;
using DecodeQrCode.Domain.Enums;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Extensions;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net;

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

        KeyDTO key = jku.Keys!.First();

        ValidateMandatoryProperties(key);

        ValidateCertficateChainProperties(key);

        if (!Enum.TryParse<JKUKeyType>(key.KeyType, out var keyType))
            throw new ServiceException(ApplicationMessage.Validate_Signature_Algorithm_NotSupported, HttpStatusCode.BadRequest);

        if (keyType == JKUKeyType.RSA)
            ValidateRSAMandatoryProperties(key);
        else if (keyType == JKUKeyType.EC)
            ValidateECMandatoryProperties(key);
    }

    private void ValidateMandatoryProperties(KeyDTO key)
    {
        List<string> mandatoryProperties = _jkuSettings.MandatoryProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (string property in mandatoryProperties)
        {
            if (typeof(KeyDTO).GetPropertyByJsonPropertyName(property) is null)
                throw new ServiceException(ApplicationMessage.Validate_JKU_Invalid, HttpStatusCode.BadRequest);
        }
    }

    private void ValidateCertficateChainProperties(KeyDTO key)
    {
        List<string> certficateChainProperties = _jkuSettings.CertficateChainProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

        bool hasAtLeastOneValidProperty = certficateChainProperties.Select(prop => typeof(KeyDTO).GetPropertyByJsonPropertyName(prop))
                                                                   .Where(p => p != null)
                                                                   .Any(p => p!.GetValue(key) != null);

        if (!hasAtLeastOneValidProperty)
            throw new ServiceException(ApplicationMessage.Validate_JKU_Invalid, HttpStatusCode.BadRequest);
    }

    private void ValidateRSAMandatoryProperties(KeyDTO key)
    {
        List<string> rsaMandatoryProperties = _jkuSettings.RSAMandatoryProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (string property in rsaMandatoryProperties)
        {
            if (typeof(KeyDTO).GetPropertyByJsonPropertyName(property) is null)
                throw new ServiceException(ApplicationMessage.Validate_JKU_Invalid, HttpStatusCode.BadRequest);
        }
    }

    private void ValidateECMandatoryProperties(KeyDTO key)
    {
        List<string> ecMandatoryProperties = _jkuSettings.ECMandatoryProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (string property in ecMandatoryProperties)
        {
            if (typeof(KeyDTO).GetPropertyByJsonPropertyName(property) is null)
                throw new ServiceException(ApplicationMessage.Validate_JKU_Invalid, HttpStatusCode.BadRequest);
        }
    }
}
