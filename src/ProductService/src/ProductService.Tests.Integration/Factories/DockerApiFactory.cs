// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;

namespace ProductService.Tests.Integration.Factories;

public class DockerApiFactory : WebApiFactory, IAsyncLifetime
{
    private readonly ConfigurationBuilder _configurationBuilder = new();
    private readonly MsSqlContainer _dbContainer;

    public DockerApiFactory()
    {
        _configurationBuilder.AddJsonFile("appsettings.Staging.json", false, true);
        var configurationRoot = _configurationBuilder.Build();
        var dockerSection = configurationRoot.GetSection("Docker");

        _dbContainer = new MsSqlBuilder()
            .WithImage(dockerSection["Image"])
            .WithPassword(dockerSection["Password"])
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = _dbContainer.GetConnectionString() + "TrustServerCertificate=True";
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var config = _configurationBuilder.Build();
            config["ConnectionStrings:DefaultConnection"] = connectionString;
            configurationBuilder.AddConfiguration(config);
        });
    }
}
