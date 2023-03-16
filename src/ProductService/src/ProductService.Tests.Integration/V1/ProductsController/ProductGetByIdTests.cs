using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Tests.Integration.Factories;

namespace ProductService.Tests.Integration.V1.ProductsController;
public class ProductGetByIdTests : BaseProductTests
{
	[Fact]
	public async Task GetProductByIdAsync_Returns_Product_When_IdExists()
	{
		//Arrange
		await using var context = ApiFactory.GetContext();

		var product = context.InitProducts().First();
		var request = product.Id;
		//Act
		var response = await Client.GetProductByIdAsync(request);
		//Assert
		response.Id.Should().Be(request);
	}
}
