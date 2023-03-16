using System.Linq.Expressions;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Contracts.SubTypes;
using ProductService.Domain;
using ProductService.Tests.Integration.Factories;

namespace ProductService.Tests.Integration.V1.ProductsController;

public class ProductPaginationTests : BaseProductTests
{
	#region OrderedRequest

	[Fact]
	public async Task GetOrderedProductsAsync_Returns_OrderedProducts_When_Ok()
	{
		//Arrange
		await using var context = ApiFactory.GetContext();
		context.InitProducts();

		var request = new FilterOrderPageRequest
		{
			FilterData = null,
			PageData = null,
			OrderByData = new OrderByData
			{
				OrderBy = "Name",
				OrderDirection = OrderDirection.Desc
			}
		};
		//Act
		var response = await Client.GetFilteredPagedProductsAsync(request);
		//Assert
		response.TotalCount.Should().Be(await context.Products.CountAsync());

		response.Data.Should().BeInDescendingOrder(p => p.Name);
	}

	[Fact]
	public async Task GetOrderedProductsAsync_Returns_ValidationException_When_ValidationRequestFailed()
	{
		//Arrange
		await using var context = ApiFactory.GetContext();
		context.InitProducts();
		var request = new FilterOrderPageRequest
		{
			FilterData = null,
			PageData = null,
			OrderByData = new OrderByData
			{
				OrderBy = "",
				OrderDirection = OrderDirection.Desc
			}
		};
		//Act
		var response = () => Client.GetFilteredPagedProductsAsync(request);
		//Assert
		var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();

		ex.Which.StatusCode.Should().Be(400);
	}
	#endregion

	#region OrderedPagedRequest

	#endregion
}