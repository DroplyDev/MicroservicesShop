// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProductService.Application.Repositories.BaseRepo;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;

namespace ProductService.Application.Repositories;

public interface IProductRepo : IBaseRepo<Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<PagedResponse<TResult>> PaginateAsync<TResult>(FilterOrderPageRequest request,
        CancellationToken cancellationToken = default) where TResult : class;
}
