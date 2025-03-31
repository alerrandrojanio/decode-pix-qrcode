namespace DecodeQrCode.Infrastructure.Configuration;

public class CacheSettings
{
    public string RedisUrl { get; set; } = string.Empty;

    public int MinutesToExpire { get; set; }
}
