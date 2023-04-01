﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Application.Caching;
public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey);
    Task SetAsync<T>(string cacheKey, T value);
    Task RemoveAsync(string cacheKey);
}