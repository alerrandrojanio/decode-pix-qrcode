using DecodeQrCode.Application.Services;
using DecodeQrCode.Application.Validators;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using DecodeQrCode.Infrastructure.Integration.Client;
using DecodeQrCode.Infrastructure.Integration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DecodeQrCode.DI.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDecodeQrCodeService, DecodeQrCodeService>();
        services.AddScoped<IDecodeService, DecodeService>();
        services.AddScoped<IDecodeQrCodeValidator, DecodeQrCodeValidator>();
        services.AddScoped<IJKUValidator, JKUValidator>();
        services.AddScoped<ISignatureValidator, SignatureValidator>();
    }

    public static void AddIntegrationServices(this IServiceCollection services)
    {
        services.AddScoped<IDecodeQrCodeIntegrationService, DecodeQrCodeIntegrationService>();
        services.AddScoped<ICertificateIntegrationService, CertificateIntegrationService>();

        services.AddScoped<IHttpClientService, HttpClientService>();

        services.AddHttpClient<HttpClientService>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QrCodeSettings>(configuration.GetSection(nameof(QrCodeSettings)));
        services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
        services.Configure<JKUSettings>(configuration.GetSection(nameof(JKUSettings)));
    }

    public static void AddRedisConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var cacheSettings = provider.GetRequiredService<IOptions<CacheSettings>>().Value;

            if (string.IsNullOrEmpty(cacheSettings.RedisUrl))
                throw new ArgumentNullException(nameof(cacheSettings.RedisUrl), "A conexão do Redis não foi configurada");

            return ConnectionMultiplexer.Connect(cacheSettings.RedisUrl);
        });
    }
}
