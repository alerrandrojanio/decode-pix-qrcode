using DecodeQrCode.Domain.Enums;
using DecodeQrCode.Domain.Exceptions;
using System.Net;
using System.Text.RegularExpressions;

namespace DecodeQrCode.Domain.Extensions;

public static class QrCodeExtensions
{
    public static PixKeyType GetPixKeyType(this string pixKey)
    {

        if (Regex.IsMatch(pixKey, @"^\d{11}$"))
            return PixKeyType.CPF;

        if (Regex.IsMatch(pixKey, @"^\d{14}$"))
            return PixKeyType.CNPJ;

        if (Regex.IsMatch(pixKey, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            return PixKeyType.EMAIL;

        if (Regex.IsMatch(pixKey, @"^\+\d{1,3}\d{8,14}$"))
            return PixKeyType.PHONE;

        if (Regex.IsMatch(pixKey, @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$"))
            return PixKeyType.EVP;

        throw new ServiceException("PixKey inválida!", HttpStatusCode.BadRequest);
    }

    public static string CalculateCRC16(string qrCodeWithoutCRC)
    {
        const ushort polynomial = 0x1021;
        
        ushort crc = 0xFFFF;

        foreach (char c in qrCodeWithoutCRC)
        {
            crc ^= (ushort)(c << 8);

            for (int i = 0; i < 8; i++)
            {
                if ((crc & 0x8000) != 0)
                    crc = (ushort)((crc << 1) ^ polynomial);
                else
                    crc <<= 1;
            }
        }

        return crc.ToString("X4");
    }
}
