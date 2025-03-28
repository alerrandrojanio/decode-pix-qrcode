using DecodeQrCode.Application.Services;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Integration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DecodeQrCode.DI.IoC;

public static class PipelineExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDecodeQrCodeService, DecodeQrCodeService>();

        services.AddScoped<IDecodeQrCodeIntegrationService, DecodeQrCodeIntegrationService>();
    }
}
