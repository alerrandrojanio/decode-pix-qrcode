using DecodeQrCode.Domain.DTOs.JKU;
using DecodeQrCode.Domain.DTOs.JKU.Objects;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.Enums;
using DecodeQrCode.Domain.Extensions;
using DecodeQrCode.Domain.Interfaces;
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
            throw new NotSupportedException("Algoritmo não suportado");

        return keyType switch
        {
            JKUKeyType.RSA => ValidateWithRSA(jku, key, dataToVerify, signature, jws.Header!.Algorithm!),
            JKUKeyType.EC => ValidateWithECC(jku, key, dataToVerify, signature, jws.Header!.Algorithm!),
            _ => throw new NotSupportedException("Algoritmo não suportado")
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
            throw new NotSupportedException("Algoritmo não suportado");

        HashAlgorithmName hashAlgorithm = algorithmType switch
        {
            RSAHashAlgorithmType.RS256 => HashAlgorithmName.SHA256,
            RSAHashAlgorithmType.RS384 => HashAlgorithmName.SHA384,
            RSAHashAlgorithmType.RS512 => HashAlgorithmName.SHA512,
            _ => throw new NotSupportedException("Algoritmo RSA não suportado")
        };

        return rsa.VerifyData(dataToVerify, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
    }

    private bool ValidateWithECC(JKUDTO jku, KeyDTO key, byte[] dataToVerify, byte[] signature, string headerAlgorithm)
    {
        using ECDsa ecdsa = ECDsa.Create();

        if (!Enum.TryParse<ECHashAlgorithmType>(headerAlgorithm, out var algorithmType))
            throw new NotSupportedException("Algoritmo não suportado");

        ECCurve curve = algorithmType switch
        {
            ECHashAlgorithmType.ES256 => ECCurve.NamedCurves.nistP256,
            ECHashAlgorithmType.ES384 => ECCurve.NamedCurves.nistP384,
            ECHashAlgorithmType.ES512 => ECCurve.NamedCurves.nistP521,
            _ => throw new NotSupportedException("Algoritmo EC não suportado")
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

        return ecdsa.VerifyData(dataToVerify, signature, HashAlgorithmName.SHA256);
    }
}
