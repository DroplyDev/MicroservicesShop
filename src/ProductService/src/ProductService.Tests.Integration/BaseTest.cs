using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Tests.Integration.Factories;

namespace ProductService.Tests.Integration;

public abstract class BaseTest
{
    protected readonly WebApiFactory ApiFactory;
    protected readonly NSwagClient Client;
    protected readonly IMapper Mapper;

    protected BaseTest()
    {
        ApiFactory = new InMemoryApiFactory();
        //ApiFactory = new DockerApiFactory();

        Client = new NSwagClient(ApiFactory.CreateClient());
        var scope = ApiFactory.Services.CreateScope();
        Mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    }
}
