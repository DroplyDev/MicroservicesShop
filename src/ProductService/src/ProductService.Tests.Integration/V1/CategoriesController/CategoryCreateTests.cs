namespace ProductService.Tests.Integration.V1.CategoriesController;

public sealed class CategoryCreateTests : BaseCategoriesTest
{
    [Fact]
    public async Task CreateCategoryAsync_Returns_CreatedProduct_When_CreatedSuccessfully()
    {
        //Arrange
        var request = new AutoFaker<CategoryCreateDto>()
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Description, f => f.Lorem.Letter(500))
            .Generate();
        //Act
        var response = await Client.CreateCategoryAsync(request);
        //Assert
        response.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task CreateCategoryAsync_Returns_ValidationException_When_WrongInputParams()
    {
        //Arrange
        var request = new AutoFaker<CategoryCreateDto>()
            .RuleFor(p => p.Name, f => f.Lorem.Letter(100))
            .RuleFor(p => p.Description, f => f.Lorem.Letter(550))
            .Generate();
        //Act
        var response = () => Client.CreateCategoryAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();
        ex.Which.StatusCode.Should().Be(400);
    }
}
