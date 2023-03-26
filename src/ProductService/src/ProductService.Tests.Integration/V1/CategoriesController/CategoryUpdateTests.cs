// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Tests.Integration.V1.CategoriesController;

public sealed class CategoryUpdateTests : BaseCategoriesTest
{
    [Fact]
    public async Task UpdateCategoryAsync_Returns_UpdateCategory_When_Ok()
    {
        //Arrange
        int categoryId;
        const string NewName = "Test";
        await using (var context = ApiFactory.GetContext())
        {
            categoryId = context.InitCategories(1).First().Id;
        }

        var categoryToUpdate = await Client.GetCategoryToUpdateByIdAsync(categoryId);
        categoryToUpdate.Name = NewName;
        //Act
        await Client.UpdateCategoryAsync(categoryId, categoryToUpdate);

        //Assert
        var category = await Client.GetCategoryByIdAsync(categoryId);

        category.Name.Should().Be(NewName);
    }

    [Fact]
    public async Task UpdateCategoryAsync_Returns_ValidationException_When_WrongInputParams()
    {
        //Arrange
        int categoryId;
        await using (var context = ApiFactory.GetContext())
        {
            categoryId = context.InitCategories(1).First().Id;
        }

        var categoryToUpdate = await Client.GetCategoryToUpdateByIdAsync(categoryId);
        categoryToUpdate.Name = new string('c', 100);
        //Act
        var response = () => Client.UpdateCategoryAsync(categoryId, categoryToUpdate);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();
        ex.Which.StatusCode.Should().Be(400);
    }
}
