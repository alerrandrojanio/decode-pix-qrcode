using DecodeQrCode.Domain.DTOs.Entities;
using DecodeQrCode.Domain.DTOs.Logging;
using DecodeQrCode.Domain.Interfaces;
using DecodeQrCode.Infrastructure.Configuration;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DecodeQrCode.Infrastructure.Logging;

public class MongoDbLogger : IDbLogger
{
    private readonly MongoDbSettings _mongoDbSettings;
    private readonly IMongoCollection<ErrorLog> _logCollection;
    private readonly ILogger<MongoDbLogger> _logger;

    public MongoDbLogger(IOptions<MongoDbSettings> mongoDbSettings, ILogger<MongoDbLogger> logger)
    {
        _mongoDbSettings = mongoDbSettings.Value;

        MongoClient client = new(_mongoDbSettings.ConnectionString);

        IMongoDatabase database = client.GetDatabase(_mongoDbSettings.DatabaseName);

        _logCollection = database.GetCollection<ErrorLog>(nameof(ErrorLog));
        _logger = logger;
    }

    public async Task SaveErrorLog(ErrorLogDTO errorLogDTO)
    {
        try
        {
            ErrorLog errorLog = errorLogDTO.Adapt<ErrorLog>();

            await _logCollection.InsertOneAsync(errorLog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro inesperado: {ex.Message}");
        }
    }
}
