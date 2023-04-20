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
global using ProductService.Contracts.SubTypes;

[assembly: TestCollectionOrderer("ProductService.Tests.Integration.ClassOrderer", "ProductService.Tests.Integration")]
