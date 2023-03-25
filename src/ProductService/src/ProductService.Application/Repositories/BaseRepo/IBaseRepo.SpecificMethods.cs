// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Linq.Expressions;
using ProductService.Contracts.Responses;
using Rusty.Template.Contracts.Requests;

#endregion

namespace ProductService.Application.Repositories.BaseRepo;

public partial interface IBaseRepo<TEntity> where TEntity : class
{
    #region Pagination

    Task<PagedResponse<TEntity>> PaginateAsync(OrderedPagedRequest request,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>?
            includes = null);

    Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderedPagedRequest request,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>
            ? includes = null) where TResult : class;

    Task<PagedResponse<TResult>> PaginateAsync<TResult>(
        OrderedPagedRequest request, Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) where TResult : class;

    #endregion
}
