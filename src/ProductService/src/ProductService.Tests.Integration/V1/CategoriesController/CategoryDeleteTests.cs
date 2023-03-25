// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Tests.Integration.V1.CategoriesController;

public sealed class CategoryDeleteTests : BaseCategoriesTest
{
    [Fact]
    public async Task DeleteCategoryAsync_Returns_NoContent_When_CategoryDeleted()
    {
        //Arrange
        int categoryId;
        await using (var context = ApiFactory.GetContext())
        {
            categoryId = context.InitCategories(1).First().Id;
        }

        //Act
        await Client.DeleteCategoryAsync(categoryId);
        //Assert
        await using (var context = ApiFactory.GetContext())
        {
            (await context.Categories.FindAsync(categoryId)).Should().BeNull();
        }
    }

    [Fact]
    public async Task DeleteCategoryAsync_Returns_NotFound_When_CategoryWithIdNotFound()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitCategories(1);
        }

        //Act
        var response = () => Client.GetCategoryByIdAsync(-1);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<ApiExceptionResponse>>();

        ex.Which.StatusCode.Should().Be(404);
    }
}
