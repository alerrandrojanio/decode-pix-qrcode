using System.Text;

namespace DecodeQrCode.Domain.Extensions;

public static class Base64Extensions
{
    public static string DecodeBase64ToString(string base64)
    {
        string paddedBase64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=')
                                    .Replace('-', '+')
                                    .Replace('_', '/');

        return Encoding.UTF8.GetString(Convert.FromBase64String(paddedBase64));
    }

    public static byte[] DecodeBase64ToBytes(string base64)
    {
        string padded = base64.Replace('-', '+').Replace('_', '/');
        
        while (padded.Length % 4 != 0)
            padded += "=";
        
        return Convert.FromBase64String(padded);
    }
}
