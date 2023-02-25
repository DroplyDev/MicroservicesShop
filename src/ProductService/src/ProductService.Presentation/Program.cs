#region

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
services.AddApiVersioningSupport(configuration);
services.AddAuth(configuration);
services.AddConfigurations(configuration);
services.AddLogging();
services.AddFluentValidation();
services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
services.AddEndpointsApiExplorer();
services.AddRepositories();
services.AddServices();
services.AddMapster();

// Build app
var app = builder.Build();
// set Serilog request logging
app.UseSerilogRequestLogging(configure =>
{
    configure.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}ms";
});
//Prepare db
if (app.Environment.IsStaging())
{
    await app.Services.CreateDatabaseFromContextIfNotExistsAsync();
    await app.Services.InitializeDatabaseDataAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var groupName in provider.ApiVersionDescriptions.Select(item => item.GroupName))
        options.SwaggerEndpoint($"../swagger/{groupName}/swagger.json",
            groupName.ToUpperInvariant());
});
app.UseRouting();
app.UseCors("All");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await app.RunAsync();
