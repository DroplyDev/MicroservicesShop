﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Infrastructure.Database;
using ProductService.Presentation;

namespace ProductService.Tests.Integration.Factories;

public class InMemoryApiFactory : WebApiFactory
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.Single(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>));
            services.Remove(descriptor);
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb")
                    .SetDefaultDbSettings();
            });
        });
        base.ConfigureWebHost(builder);
    }
}
