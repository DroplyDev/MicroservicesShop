namespace ProductService.Tests.Integration.V1.ProductsController;

public sealed class ProductGetByIdTest : BaseProductTest
{
    [Fact]
    public async Task GetProductByIdAsync_Returns_Product_When_IdExists()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();

        var productId = context.InitProducts().First().Id;
        //Act
        var response = await Client.GetProductByIdAsync(productId);
        //Assert
        response.Id.Should().Be(productId);
    }

    [Fact]
    public async Task GetProductByIdAsync_Returns_NotFound_When_ProductWithIdNotFound()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();

        context.InitProducts();
        var request = -1;
        //Act
        var response = () => Client.GetProductByIdAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<ApiExceptionResponse>>();

        ex.Which.StatusCode.Should().Be(404);
    }
}
