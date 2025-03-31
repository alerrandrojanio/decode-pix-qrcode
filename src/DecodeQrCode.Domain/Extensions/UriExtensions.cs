namespace DecodeQrCode.Domain.Extensions;

public static class UriExtensions
{
    public static string AddSecurityPrefix(this string uri)
    {
        if (!uri.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !uri.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return $"https://{uri}";
        }

        return uri;
    }
}
