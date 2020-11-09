using CacheManager.Core;
using MediatR;
using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CachePipelineExample.Cache
{
    public sealed class CommandClearer<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandClearer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (typeof(TRequest).GetInterfaces().Contains(typeof(IClearAllCache)))
            {
                var types = typeof(TRequest).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IBaseCacheResponse<>))).ToList();
                foreach (var type in types)
                {
                    var response = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition().Equals(typeof(IRequest<>))).GetGenericArguments().First();
                    dynamic cacheManager = _serviceProvider.GetService(typeof(ICacheManager<>).MakeGenericType(response));
                    cacheManager.Clear();
                }
            }
            if (typeof(TRequest).GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition().Equals(typeof(IClearSomeCache<,>))))
            {
                var types = typeof(TRequest).GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition().Equals(typeof(IClearSomeCache<,>)))
                    .Select(x => x.GetGenericArguments().First());
                foreach (var type in types)
                {
                    var response = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition().Equals(typeof(IRequest<>))).GetGenericArguments().First();

                    dynamic cacheManager = _serviceProvider.GetService(typeof(ICacheManager<>).MakeGenericType(type));
                    cacheManager.Clear();
                }

            }

            return next.Invoke();
        }
        private string Key(TRequest request)
        {
            return JsonSerializer.Serialize(request);
        }
    }
}