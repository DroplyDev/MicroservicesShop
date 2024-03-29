﻿using ProductService.Application.Repositories.BaseRepo;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;

namespace ProductService.Application.Repositories;

public interface IProductRepo : IBaseGenericRepo<Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<PagedResponse<TResult>> PaginateAsync<TResult>(FilterOrderPageRequest request,
        CancellationToken cancellationToken = default) where TResult : class;
}
