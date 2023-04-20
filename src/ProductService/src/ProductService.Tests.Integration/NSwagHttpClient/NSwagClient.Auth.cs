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
