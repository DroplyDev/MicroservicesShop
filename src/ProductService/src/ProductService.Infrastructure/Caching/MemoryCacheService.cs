// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ProductService.Application.Caching;
using ProductService.Infrastructure.Options;

namespace ProductService.Infrastructure.Caching;
public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cacheOptions;
    public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheConfiguration> cacheOptions)
    {
        _memoryCache = memoryCache;
        var cacheConfig = cacheOptions.Value;
        _cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.Add(TimeSpan.FromTicks(cacheConfig.AbsoluteExpirationLifetime.Ticks)),
            SlidingExpiration = !cacheConfig.SlidingExpirationLifetime.HasValue ? null : TimeSpan.FromTicks(cacheConfig.AbsoluteExpirationLifetime.Ticks),
            Priority = CacheItemPriority.High,
        };
    }
    public Task<T?> GetAsync<T>(string cacheKey)
    {
        return Task.FromResult(_memoryCache.Get<T>(cacheKey));
    }

    public Task SetAsync<T>(string cacheKey, T value)
    {
        _memoryCache.Set(cacheKey, value, _cacheOptions);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
        return Task.CompletedTask;
    }
}
