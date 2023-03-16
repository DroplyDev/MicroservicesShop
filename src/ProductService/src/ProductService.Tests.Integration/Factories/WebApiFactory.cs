using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
