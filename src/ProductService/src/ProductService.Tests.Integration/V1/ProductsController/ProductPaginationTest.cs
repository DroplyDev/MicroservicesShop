// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using ProductService.Contracts.SubTypes;

namespace ProductService.Tests.Integration.V1.ProductsController;

public sealed class ProductPaginationTest : BaseProductTest
{
    #region OrderedRequest

    [Fact]
    public async Task GetOrderedProductsAsync_Returns_OrderedProducts_When_Ok()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitProducts();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = null,
            OrderByData = new OrderByData {OrderBy = "Name", OrderDirection = OrderDirection.Desc}
        };
        //Act
        var response = await Client.GetFilteredPagedProductsAsync(request);
        //Assert
        await using (var context = ApiFactory.GetContext())
        {
            response.TotalCount.Should().Be(await context.Products.CountAsync());
        }

        response.Data.Should().BeInDescendingOrder(p => p.Name);
    }

    [Fact]
    public async Task GetOrderedProductsAsync_Returns_ValidationException_When_ValidationRequestFailed()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitProducts();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = null,
            OrderByData = new OrderByData {OrderBy = "", OrderDirection = OrderDirection.Desc}
        };
        //Act
        var response = () => Client.GetFilteredPagedProductsAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();

        ex.Which.StatusCode.Should().Be(400);
    }

    #endregion

    #region OrderedPagedRequest

    [Fact]
    public async Task GetOrderedPagedProductsAsync_Returns_OrderedProducts_When_Ok()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitProducts();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = new PageData {Offset = 1, Limit = 2},
            OrderByData = new OrderByData {OrderBy = "Name", OrderDirection = OrderDirection.Desc}
        };
        //Act

        var response = await Client.GetFilteredPagedProductsAsync(request);
        //Assert
        response.Data.Should().HaveCount(2);

        await using (var context = ApiFactory.GetContext())
        {
            response.TotalCount.Should().Be(await context.Products.CountAsync());
        }

        response.Data.Should().BeInDescendingOrder(p => p.Name);
    }

    [Fact]
    public async Task GetOrderedPagedProductsAsync_Returns_ValidationException_When_ValidationRequestFailed()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();
        context.InitProducts();
        var request = new FilterOrderPageRequest
        {
            FilterData = null,
            PageData = new PageData {Offset = -1, Limit = -1},
            OrderByData = new OrderByData {OrderBy = "Name", OrderDirection = OrderDirection.Desc}
        };
        //Act
        var response = () => Client.GetFilteredPagedProductsAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();

        ex.Which.StatusCode.Should().Be(400);
    }

    #endregion

    #region FilteredOrderedPagedRequest

    [Fact]
    public async Task GetFilteredOrderedProductsAsync_Returns_OrderedProducts_When_Ok()
    {
        //Arrange
        await using (var context = ApiFactory.GetContext())
        {
            context.InitProducts();
        }

        var request = new FilterOrderPageRequest
        {
            FilterData = new FilterData {DateFrom = DateTime.MinValue, DateTo = DateTime.Now},
            OrderByData = new OrderByData {OrderBy = "Name", OrderDirection = OrderDirection.Desc}
        };
        //Act

        var response = await Client.GetFilteredPagedProductsAsync(request);
        //Assert

        await using (var context = ApiFactory.GetContext())
        {
            response.TotalCount.Should().Be(await context.Products.CountAsync());
        }

        response.Data.Should().BeInDescendingOrder(p => p.Name);
    }

    [Fact]
    public async Task GetFilteredOrderedProductsAsync_Returns_ValidationException_When_ValidationRequestFailed()
    {
        //Arrange
        await using var context = ApiFactory.GetContext();
        context.InitProducts();
        var request = new FilterOrderPageRequest
        {
            FilterData = new FilterData {DateFrom = DateTime.Now.AddDays(1), DateTo = DateTime.Now.AddDays(-1)},
            OrderByData = new OrderByData {OrderBy = "Name", OrderDirection = OrderDirection.Desc}
        };
        //Act
        var response = () => Client.GetFilteredPagedProductsAsync(request);
        //Assert
        var ex = await response.Should().ThrowExactlyAsync<SwaggerException<BadRequestResponse>>();

        ex.Which.StatusCode.Should().Be(400);
    }

    #endregion
}
