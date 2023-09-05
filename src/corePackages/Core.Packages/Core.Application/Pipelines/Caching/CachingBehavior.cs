using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly CacheSettings _cacheSettings;
    private readonly IDistributedCache _distributedCache;

    public CachingBehavior(CacheSettings cacheSettings, IDistributedCache distributedCache)
    {
        _cacheSettings = cacheSettings;
        _distributedCache = distributedCache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.ByPassCache)
        {
            return await next();
        }

        TResponse response;
        byte[]? cachedResponse = await _distributedCache.GetAsync(request.CacheKey, cancellationToken);
        if (cachedResponse == null)
        {
            response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));
        }
        else
        {
            response = await next();
            TimeSpan slidingExpiration=request.SlidingExpiration??TimeSpan.FromDays(1);
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
            {
                SlidingExpiration=slidingExpiration
            };
            byte[] serializeData=Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));
            await _distributedCache.SetAsync(request.CacheKey, serializeData, cancellationToken);
            return response;
        }
        return response;
    }
}
