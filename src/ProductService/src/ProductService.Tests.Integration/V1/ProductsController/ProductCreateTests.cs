using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBogus;
using Microsoft.EntityFrameworkCore;
using ProductService.Tests.Integration.Factories;

namespace ProductService.Tests.Integration.V1.ProductsController;
public class ProductCreateTests : BaseProductTests
{
	[Fact]
	public async Task CreateProductAsync_Returns_CreatedProduct_When_CreatedSuccessfully()
	{
		//Arrange
		await using var context = ApiFactory.GetContext();

		var categoryId = context.InitCategories(1).First().Id;
		var request = new AutoFaker<ProductCreateDto>()
			.RuleFor(p => p.Name, f => f.Commerce.ProductName())
			.RuleFor(p => p.Description, f => f.Lorem.Letter(500))
			.RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
			.RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
			.RuleFor(p => p.CategoryId, _ => categoryId)
			.Generate();
		//Act
		var response = await Client.CreateProductAsync(request);
		//Assert
		response.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateProductAsync_Returns_ValidationException_When_WrongInputParams()
	{
		//Arrange
		await using var context = ApiFactory.GetContext();

		var categoryId = context.InitCategories(1).First().Id;
		var request = new AutoFaker<ProductCreateDto>()
			.RuleFor(p => p.Name, f => f.Lorem.Letter(100))
			.RuleFor(p => p.Description, f => f.Lorem.Letter(550))
			.RuleFor(p => p.Quantity, f => -1)
			.RuleFor(p => p.Price, f => 0)
			.RuleFor(p => p.CategoryId, _ => categoryId)
			.Generate();
		//Act
		var response = () => Client.CreateProductAsync(request);
		//Assert
		var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();
		ex.Which.StatusCode.Should().Be(400);
	}
}
