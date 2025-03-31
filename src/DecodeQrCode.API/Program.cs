using DecodeQrCode.API.Mapping;
using DecodeQrCode.API.Middlewares;
using DecodeQrCode.DI.IoC;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();

builder.Services.AddIntegrationServices();

builder.Services.ConfigureSettings(builder.Configuration);

builder.Services.AddRedisConnection(builder.Configuration);

MappingConfiguration.RegisterMappings();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = ApiVersion.Default;
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.Run();
