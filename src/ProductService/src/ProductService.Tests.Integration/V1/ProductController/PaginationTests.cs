using Microsoft.Extensions.DependencyInjection;
using ProductService.Contracts.SubTypes;
using ProductService.Infrastructure.Database;

namespace ProductService.Tests.Integration.V1.ProductController;

public class PaginationTests : BaseTests
{
	public PaginationTests(WebApiFactory apiFactory) : base(apiFactory)
	{
		using var scope = ApiFactory.Services.CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		context.InitProducts();
	}

	[Fact]
	public async Task GetFilteredPagedProductsAsync_Returns_Ok_When_Ok()
	{
		//Arrange
		var request = new FilterOrderPageRequest
		{
			FilterData = null,
			OrderByData = new OrderByData
			{
				OrderBy = "Name",
				OrderDirection = OrderDirection.Desc
			}
		};
		//Act
		var response = await Client.GetFilteredPagedProductsAsync(request);
		//Assert
		response.TotalCount.Should().Be(100);
	}

	[Fact]
	public async Task GetFilteredPagedProductsAsync_Returns_ValidationException_When_ValidationRequestFailed()
	{
		//Arrange
		var request = new FilterOrderPageRequest
		{
			FilterData = null,
			OrderByData = new OrderByData
			{
				OrderBy = "",
				OrderDirection = OrderDirection.Desc
			}
		};
		//Act
		var response = await Client.GetFilteredPagedProductsAsync(request);
		//Assert

	}
}