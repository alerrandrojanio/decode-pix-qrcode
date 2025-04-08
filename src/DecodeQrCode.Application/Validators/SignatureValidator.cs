using DecodeQrCode.Application.Resources;
using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.DTOs.JKU.Objects;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.Enums;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Extensions;
using DecodeQrCode.Domain.Interfaces;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace DecodeQrCode.Application.Validators;

public class SignatureValidator : ISignatureValidator
{
    public bool Validate(JWSDTO jws, JKUDTO jku)
    {
        string[] parts = jws.JWS!.Split('.');

        string headerBase64 = parts[(int)JWSParts.HEADER];
        string payloadBase64 = parts[(int)JWSParts.PAYLOAD];
        string signatureBase64 = parts[(int)JWSParts.SIGNATURE];

        byte[] signature = Base64Extensions.DecodeBase64ToBytes(signatureBase64);
        byte[] dataToVerify = Encoding.UTF8.GetBytes($"{headerBase64}.{payloadBase64}");

        KeyDTO key = jku.Keys!.First();

        if (!Enum.TryParse<JKUKeyType>(key.KeyType, out var keyType))
            throw new ServiceException(ApplicationMessage.Validate_Signature_Algorithm_NotSupported, HttpStatusCode.BadRequest);

        return keyType switch
        {
            JKUKeyType.RSA => ValidateWithRSA(jku, key, dataToVerify, signature, jws.Header!.Algorithm!),
            JKUKeyType.EC => ValidateWithECC(jku, key, dataToVerify, signature, jws.Header!.Algorithm!),
            _ => throw new ServiceException(ApplicationMessage.Validate_Signature_Algorithm_NotSupported, HttpStatusCode.BadRequest)
        };
    }

    private bool ValidateWithRSA(JKUDTO jku, KeyDTO key, byte[] dataToVerify, byte[] signature, string headerAlgorithm)
    {
        using RSA rsa = RSA.Create();

        RSAParameters rsaParameters = new()
        {
            Modulus = Base64Extensions.DecodeBase64ToBytes(key.Modulus!),
            Exponent = Base64Extensions.DecodeBase64ToBytes(key.Exponent!)
        };

        rsa.ImportParameters(rsaParameters);

        if (!Enum.TryParse<RSAHashAlgorithmType>(headerAlgorithm, out var algorithmType))
            throw new ServiceException(ApplicationMessage.Validate_Signature_Algorithm_NotSupported, HttpStatusCode.BadRequest);

        (HashAlgorithmName hashAlgorithm, RSASignaturePadding rsaSignaturePadding) = algorithmType switch
        {
            RSAHashAlgorithmType.RS256 => (HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1),
            RSAHashAlgorithmType.RS384 => (HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1),
            RSAHashAlgorithmType.RS512 => (HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1),
            RSAHashAlgorithmType.PS256 => (HashAlgorithmName.SHA256, RSASignaturePadding.Pss),
            RSAHashAlgorithmType.PS384 => (HashAlgorithmName.SHA384, RSASignaturePadding.Pss),
            RSAHashAlgorithmType.PS512 => (HashAlgorithmName.SHA512, RSASignaturePadding.Pss),
            _ => throw new ServiceException(ApplicationMessage.Validate_Signature_RSA_NotSupported, HttpStatusCode.BadRequest)
        };

        return rsa.VerifyData(dataToVerify, signature, hashAlgorithm, rsaSignaturePadding);
    }

    private bool ValidateWithECC(JKUDTO jku, KeyDTO key, byte[] dataToVerify, byte[] signature, string headerAlgorithm)
    {
        using ECDsa ecdsa = ECDsa.Create();

        if (!Enum.TryParse<ECHashAlgorithmType>(headerAlgorithm, out var algorithmType))
            throw new ServiceException(ApplicationMessage.Validate_Signature_Algorithm_NotSupported, HttpStatusCode.BadRequest);

        (ECCurve curve, HashAlgorithmName hashAlgorithm) = algorithmType switch
        {
            ECHashAlgorithmType.ES256 => (ECCurve.NamedCurves.nistP256, HashAlgorithmName.SHA256),
            ECHashAlgorithmType.ES384 => (ECCurve.NamedCurves.nistP384, HashAlgorithmName.SHA384),
            ECHashAlgorithmType.ES512 => (ECCurve.NamedCurves.nistP521, HashAlgorithmName.SHA512),
            _ => throw new ServiceException(ApplicationMessage.Validate_Signature_EC_NotSupported, HttpStatusCode.BadRequest)
        };

        ecdsa.ImportParameters(new ECParameters
        {
            Curve = curve,
            Q = new ECPoint
            {
                X = Base64Extensions.DecodeBase64ToBytes(key.XCoordinate!),
                Y = Base64Extensions.DecodeBase64ToBytes(key.YCoordinate!)
            }
        });

        return ecdsa.VerifyData(dataToVerify, signature, hashAlgorithm);
    }
}
