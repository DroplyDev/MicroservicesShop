// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

global using FluentAssertions;
global using Xunit;
global using ProductService.Contracts.Responses;
global using ProductService.Contracts.Dtos.Categories;
global using ProductService.Contracts.Dtos.ProductImage;
global using ProductService.Contracts.Dtos.Products;
global using ProductService.Contracts.Requests.Pagination;
global using ProductService.Contracts.Responses.Paged;
global using ProductService.Tests.Shared;
global using AutoBogus;

#endregion

[assembly: TestCollectionOrderer("ProductService.Tests.Integration.ClassOrderer", "ProductService.Tests.Integration")]
