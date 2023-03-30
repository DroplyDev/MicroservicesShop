﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ProductService.Application.Cache;
using ProductService.Infrastructure.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductService.Infrastructure.Cache;
public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public RedisCacheService(IDistributedCache cache, IOptions<CacheConfiguration> cacheOptions)
    {
        _cache = cache;
        var cacheConfig = cacheOptions.Value;
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.Add(TimeSpan.FromTicks(cacheConfig.AbsoluteExpirationLifetime.Ticks)),
            SlidingExpiration = !cacheConfig.SlidingExpirationLifetime.HasValue ? null : TimeSpan.FromTicks(cacheConfig.AbsoluteExpirationLifetime.Ticks)
        };
    }

    public async Task<T?> GetAsync<T>(string cacheKey)
    {
        var jsonData = await _cache.GetStringAsync(cacheKey);
        return jsonData is null ? default : JsonSerializer.Deserialize<T>(jsonData);
    }

    public Task SetAsync<T>(string cacheKey, T value)
    {
        var jsonData = JsonSerializer.Serialize(value);
        return _cache.SetStringAsync(cacheKey, jsonData, _cacheOptions);
    }
    public Task RemoveAsync(string cacheKey)
    {
        return _cache.RemoveAsync(cacheKey);
    }
}
