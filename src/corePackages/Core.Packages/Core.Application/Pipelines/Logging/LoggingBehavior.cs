using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.SeriLog;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.Application.Pipelines.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly BaseLoggerService _baseLoggerService;

    public LoggingBehavior(BaseLoggerService baseLoggerService, IHttpContextAccessor contextAccessor)
    {
        _baseLoggerService = baseLoggerService;
        _contextAccessor = contextAccessor;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        List<LogParameter> logParameters = new()
        {
            new LogParameter { Type = request.GetType().Name, Value = request }
        };

        LogDetail logDetail = new()
        {
            MethodName = next.Method.Name,
            Parameters = logParameters,
            User = _contextAccessor.HttpContext.User.Identity?.Name ?? "No user"
        };

        _baseLoggerService.Information(JsonSerializer.Serialize(logDetail));
        return await next();
    }
}
