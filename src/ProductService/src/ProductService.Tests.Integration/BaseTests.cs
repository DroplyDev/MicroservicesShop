using FluentAssertions.Common;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Infrastructure.Database;
using ProductService.Tests.Integration.Factories;

namespace ProductService.Tests.Integration;

public abstract class BaseTests
{
	protected readonly WebApiFactory ApiFactory;
	protected readonly NSwagClient Client;
	protected readonly IMapper Mapper;

	protected BaseTests()
	{
		ApiFactory = new InMemoryApiFactory();
		Client = new NSwagClient(ApiFactory.CreateClient());
		var scope = ApiFactory.Services.CreateScope();
		Mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
	}
}