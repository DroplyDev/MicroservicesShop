namespace ProductService.Tests.Integration;

public abstract class BaseTests : IClassFixture<WebApiFactory>
{
	protected readonly WebApiFactory ApiFactory;
	protected readonly NSwagClient Client;

	protected BaseTests(WebApiFactory apiFactory)
	{
		ApiFactory = apiFactory;
		//Client = new NSwagClient(apiFactory.CreateClient());
	}
}