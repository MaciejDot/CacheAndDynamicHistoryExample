using CacheManager.Core;
using MediatR;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CachePipelineExample.Cache
{
    public sealed class QueryCacher<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICacheManager<TResponse> _cacheManager;

        public QueryCacher(ICacheManager<TResponse> cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (typeof(TRequest).GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition().Equals(typeof(ICacheResponse<>))))
            {
                var key = Key(request);
                if (!_cacheManager.Exists(key))
                {
                    var response = await next.Invoke();
                    _cacheManager.Add(key, response);
                    return response;
                }
                return _cacheManager.Get(key);
            }
            return await next.Invoke();
        }

        private string Key(TRequest request)
        {
            return JsonSerializer.Serialize(request);
        }
    }
}