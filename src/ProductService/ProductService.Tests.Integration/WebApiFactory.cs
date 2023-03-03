﻿using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using ProductService.Presentation;

namespace ProductService.Tests.Integration;

public class WebApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
	private readonly ConfigurationBuilder _configurationBuilder = new();
	private readonly MsSqlTestcontainer _dbContainer;

	public WebApiFactory()
	{
		_configurationBuilder.AddJsonFile("appsettings.Staging.json", false, true);
		var configurationRoot = _configurationBuilder.Build();
		var dockerSection = configurationRoot.GetSection("Docker");

		_dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
			.WithDatabase(new MsSqlTestcontainerConfiguration(dockerSection["Image"])
			{
				Password = dockerSection["Password"]
			}).Build();
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
		var connectionString = _dbContainer.ConnectionString + "TrustServerCertificate=True";
		builder.ConfigureAppConfiguration(configurationBuilder =>
		{
			var config = _configurationBuilder.Build();
			config["ConnectionStrings:DefaultConnection"] = connectionString;
			configurationBuilder.AddConfiguration(config);
		});
	}
}