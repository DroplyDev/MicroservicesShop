// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Tests.Integration.V1.ProductsController;

public sealed class ProductDeleteTest : BaseProductTest
{
    [Fact]
    public async Task DeleteProductAsync_Returns_NoContent_When_ProductDeleted()
    {
        //Arrange
        int productId;
        await using (var context = ApiFactory.GetContext())
        {
            productId = context.InitProducts(1).First().Id;
        }

        //Act
        await Client.DeleteProductAsync(productId);
        //Assert
        await using (var context = ApiFactory.GetContext())
        {
            (await context.Products.FindAsync(productId)).Should().BeNull();
        }
    }

    [Fact]
    public async Task DeleteProductAsync_Returns_NotFound_When_ProductWithIdNotFound()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitProducts(1);
        }

        //Act
        var response = () => Client.GetProductByIdAsync(-1);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<ApiExceptionResponse>>();

        ex.Which.StatusCode.Should().Be(404);
    }
}
