using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.SeriLog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly IHttpContextAccessor _htpContextAccessor;
    private readonly BaseLoggerService _baseLoggerService;
    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor htpContextAccessor, BaseLoggerService baseLoggerService)
    {
        _httpExceptionHandler = new HttpExceptionHandler();
        _next = next;
        _htpContextAccessor = htpContextAccessor;
        _baseLoggerService = baseLoggerService;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = context.Response;
            await LogException(context,ex);
            await _httpExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    private Task LogException(HttpContext context, Exception ex)
    {
        var logParameters=new List<LogParameter>()
        {
            new LogParameter(){Type=context.GetType().Name,Value=ex.ToString()}
        };

        var logDetail = new LogDetailWithException() { 
            MethodName=_next.Method.Name,
            Parameters = logParameters,
            ExceptionMessage=ex.Message,
            User=_htpContextAccessor.HttpContext.User.Identity?.Name??""
        };
        _baseLoggerService.Error(JsonSerializer.Serialize(logDetail));
        return Task.CompletedTask;
    }
}
