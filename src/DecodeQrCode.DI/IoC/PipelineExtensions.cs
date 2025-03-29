﻿using DecodeQrCode.Application.Services;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using DecodeQrCode.Infrastructure.Integration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DecodeQrCode.DI.IoC;

public static class PipelineExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDecodeQrCodeService, DecodeQrCodeService>();
        services.AddScoped<IDecodeService, DecodeService>();
        services.AddScoped<IDecodeQrCodeIntegrationService, DecodeQrCodeIntegrationService>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QrCodeSettings>(configuration.GetSection(nameof(QrCodeSettings)));
    }
}
