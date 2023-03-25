// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Tests.Integration.V1.ProductsController;

public sealed class ProductUpdateTest : BaseProductTest
{
    [Fact]
    public async Task UpdateProductAsync_Returns_UpdateProduct_When_Ok()
    {
        //Arrange
        int productId;
        const string NewName = "Test";
        await using (var context = ApiFactory.GetContext())
        {
            productId = context.InitProducts(1).First().Id;
        }

        var productToUpdate = await Client.GetProductToUpdateByIdAsync(productId);
        productToUpdate.Name = NewName;
        //Act
        await Client.UpdateProductAsync(productId, productToUpdate);

        //Assert
        var product = await Client.GetProductByIdAsync(productId);

        product.Name.Should().Be(NewName);
    }

    [Fact]
    public async Task UpdateProductAsync_Returns_ValidationException_When_WrongInputParams()
    {
        //Arrange
        int productId;
        await using (var context = ApiFactory.GetContext())
        {
            productId = context.InitProducts(1).First().Id;
        }

        var productToUpdate = await Client.GetProductToUpdateByIdAsync(productId);
        productToUpdate.Name = new string('c', 100);
        //Act
        var response = () => Client.UpdateProductAsync(productId, productToUpdate);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();
        ex.Which.StatusCode.Should().Be(400);
    }
}
