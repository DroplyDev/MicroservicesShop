using Microsoft.AspNetCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebHost.CreateDefaultBuilder(args);
builder.UseKestrel();


builder.UseContentRoot(Directory.GetCurrentDirectory());
builder.ConfigureAppConfiguration((hostingContext, config) =>
{
	config
		.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
		.AddJsonFile("appsettings.json", true, true)
		.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
		.AddJsonFile("ocelot.json")
		.AddEnvironmentVariables();
});
builder.ConfigureServices(s => { s.AddOcelot(); });
builder.UseIISIntegration();
builder.Configure(app => { app.UseOcelot().Wait(); });

var app = builder.Build();

await app.RunAsync();