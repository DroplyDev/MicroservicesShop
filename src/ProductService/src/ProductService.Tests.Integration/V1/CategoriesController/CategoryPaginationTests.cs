// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using ProductService.Contracts.SubTypes;

namespace ProductService.Tests.Integration.V1.CategoriesController;

public sealed class CategoryPaginationTests : BaseCategoriesTest
{
    #region OrderedRequest

    [Fact]
    public async Task GetOrderedCategoriesAsync_Returns_OrderedCategories_When_Ok()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitCategories();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = null,
            OrderByData = new OrderByData { OrderBy = "Name", OrderDirection = OrderDirection.Desc }
        };
        //Act
        var response = await Client.GetFilteredPagedCategoriesAsync(request);
        //Assert
        await using (var context = ApiFactory.GetContext())
        {
            response.TotalCount.Should().Be(await context.Categories.CountAsync());
        }

        response.Data.Should().BeInDescendingOrder(p => p.Name);
    }

    [Fact]
    public async Task GetOrderedCategoriesAsync_Returns_ValidationException_When_ValidationRequestFailed()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitCategories();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = null,
            OrderByData = new OrderByData { OrderBy = "", OrderDirection = OrderDirection.Desc }
        };
        //Act
        var response = () => Client.GetFilteredPagedCategoriesAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();

        ex.Which.StatusCode.Should().Be(400);
    }

    #endregion

    #region OrderedPagedRequest

    [Fact]
    public async Task GetOrderedPagedCategoriesAsync_Returns_OrderedCategories_When_Ok()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitCategories();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = new PageData { Offset = 1, Limit = 2 },
            OrderByData = new OrderByData { OrderBy = "Name", OrderDirection = OrderDirection.Desc }
        };
        //Act

        var response = await Client.GetFilteredPagedCategoriesAsync(request);
        //Assert
        response.Data.Should().HaveCount(2);

        await using (var context = ApiFactory.GetContext())
        {
            response.TotalCount.Should().Be(await context.Categories.CountAsync());
        }

        response.Data.Should().BeInDescendingOrder(p => p.Name);
    }

    [Fact]
    public async Task GetOrderedPagedCategoriesAsync_Returns_ValidationException_When_ValidationRequestFailed()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();
        context.InitCategories();
        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = new PageData { Offset = -1, Limit = -1 },
            OrderByData = new OrderByData { OrderBy = "Name", OrderDirection = OrderDirection.Desc }
        };
        //Act
        var response = () => Client.GetFilteredPagedCategoriesAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();

        ex.Which.StatusCode.Should().Be(400);
    }

    #endregion

    #region FilteredOrderedPagedRequest

    [Fact]
    public async Task GetFilteredOrderedCategoriesAsync_Returns_OrderedCategories_When_Ok()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitCategories();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = new FilterData { DateFrom = DateTime.MinValue, DateTo = DateTime.Now },
            OrderByData = new OrderByData { OrderBy = "Name", OrderDirection = OrderDirection.Desc }
        };
        //Act

        var response = await Client.GetFilteredPagedCategoriesAsync(request);
        //Assert

        await using (var context = ApiFactory.GetContext())
        {
            response.TotalCount.Should().Be(await context.Categories.CountAsync());
        }

        response.Data.Should().BeInDescendingOrder(p => p.Name);
    }

    [Fact]
    public async Task GetFilteredOrderedCategoriesAsync_Returns_ValidationException_When_ValidationRequestFailed()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();
        context.InitCategories();
        var request = new FilterOrderPageRequest
        {
            FilterData = new FilterData { DateFrom = DateTime.Now.AddDays(1), DateTo = DateTime.Now.AddDays(-1) },
            OrderByData = new OrderByData { OrderBy = "Name", OrderDirection = OrderDirection.Desc }
        };
        //Act
        var response = () => Client.GetFilteredPagedCategoriesAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();

        ex.Which.StatusCode.Should().Be(400);
    }

    #endregion
}
