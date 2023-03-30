#region

using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using ProductService.Infrastructure.Database;
using ProductService.Infrastructure.Middlewares;
using ProductService.Presentation;
using Serilog;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Host.AddSerilog();
var configuration = builder.Configuration;
var services = builder.Services;

services.AddDatabases(configuration, builder.Environment);
services.AddSwagger(configuration);
services.AddApiVersioningSupport();
services.AddAuth(configuration);
services.AddConfigurations(configuration);
services.AddLogging();
services.AddFluentValidation();
services.AddControllers(options => options.Conventions.Add(new KebabCaseControllerModelConvention()))
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
services.AddEndpointsApiExplorer();
services.AddRepositories();
services.AddServices();
services.AddMapster();
services.AddMediatorService();
services.AddHttpContextAccessor();
services.AddRouting();
services.AddCaching(configuration);
// Add useful interface for accessing the ActionContext outside a controller.
services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
// Build app
WebApplication app = null!;
try
{
    Log.Information("Initializing");
    app = builder.Build();
    // set Serilog request logging
    app.UseSerilogRequestLogging(configure =>
    {
        configure.MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}ms";
    });
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var groupName in provider.ApiVersionDescriptions.Select(item => item.GroupName))
        {
            options.SwaggerEndpoint($"../swagger/{groupName}/swagger.json",
                groupName.ToUpperInvariant());
        }
    });
    app.UseRouting();
    app.UseCors("All");

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync().ConfigureAwait(false);
    app.LogApplicationStopped();
}
catch (Exception exception)
{
    app.LogApplicationTerminatedUnexpectedly(exception);
}
finally
{
    await Log.CloseAndFlushAsync();
}
