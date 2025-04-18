using DecodeQrCode.API.Resources;
using DecodeQrCode.Domain.DTOs.Logging;
using DecodeQrCode.Domain.Exceptions;
using DecodeQrCode.Domain.Interfaces;
using Mapster;
using System.Net;
using System.Text.Json;

namespace DecodeQrCode.API.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is ServiceException serviceException)
                await HandleExceptionAsync(context, serviceException);
            else if (ex is DecodeException decodeException)
            {
                await SaveLog(decodeException.InnerException!);
                await HandleExceptionAsync(context, decodeException);
            }
            else
            {
                await SaveLog(ex);
                await HandleExceptionAsync(context);
            }

            if (!context.Response.HasStarted)
                throw;
        }
    }

    private async Task HandleExceptionAsync(HttpContext context)
    {
        await WriteErrorResponse(context, HttpStatusCode.InternalServerError, APIMessage.Error_GenericError);
    }

    private async Task HandleExceptionAsync(HttpContext context, ServiceException exception)
    {
        await WriteErrorResponse(context, exception.StatusCode, exception.Message);
    }

    private async Task HandleExceptionAsync(HttpContext context, DecodeException exception)
    {
        await WriteErrorResponse(context, exception.StatusCode, exception.Message);
    }

    private async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        object errorResponse = new
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        string result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }

    private async Task SaveLog(Exception ex)
    {
        using (IServiceScope scope = _scopeFactory.CreateScope())
        {
            IDbLogger scopedMongoDbLogger = scope.ServiceProvider.GetRequiredService<IDbLogger>();

            ErrorLogDTO errorLogDTO = ex.Adapt<ErrorLogDTO>();

            await scopedMongoDbLogger.SaveErrorLog(errorLogDTO);
        }
    }
}
