region

using System.Linq.Expressions;
using ProductService.Contracts.Responses;
using Rusty.Template.Contracts.Requests;

#endregion

namespace ProductService.Application.Repositories.BaseRepo;

public partial interface IBaseGenericRepo<TEntity> where TEntity : class
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
