using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Infrastructure.Database;
using ProductService.Presentation;

namespace ProductService.Tests.Integration.Factories;

public abstract class WebApiFactory : WebApplicationFactory<IApiMarker>
{
    public AppDbContext GetContext()
    {
        return Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
    }
}
