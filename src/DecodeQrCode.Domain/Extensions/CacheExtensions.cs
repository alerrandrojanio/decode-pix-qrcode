namespace DecodeQrCode.Domain.Extensions;

public static class CacheExtensions
{
    public static string GenerateKey<T>(string type, string value)
    {
        string entityType = typeof(T).Name;

        return $"{entityType}_{type}_{value}";
    }
}
