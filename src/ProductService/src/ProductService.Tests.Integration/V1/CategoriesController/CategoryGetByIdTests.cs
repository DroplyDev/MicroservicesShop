namespace ProductService.Tests.Integration.V1.CategoriesController;

public sealed class CategoryGetByIdTests : BaseCategoriesTest
{
    [Fact]
    public async Task GetProductByIdAsync_Returns_Product_When_IdExists()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();

        var categoryId = context.InitCategories().First().Id;
        //Act
        var response = await Client.GetCategoryByIdAsync(categoryId);
        //Assert
        response.Id.Should().Be(categoryId);
    }

    [Fact]
    public async Task GetProductByIdAsync_Returns_NotFound_When_ProductWithIdNotFound()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();

        context.InitCategories();
        var request = -1;
        //Act
        var response = () => Client.GetCategoryByIdAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<ApiExceptionResponse>>();

        ex.Which.StatusCode.Should().Be(404);
    }
}
