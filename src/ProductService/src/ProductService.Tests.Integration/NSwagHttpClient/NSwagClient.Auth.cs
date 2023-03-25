// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProductService.Tests.Integration;

public partial class NSwagClient
{
    public Task AuthenticateAsync(string username, string password)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, "BearerTokenExample");
        return Task.CompletedTask;
    }
}
