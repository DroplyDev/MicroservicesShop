#region

using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ProductService.Contracts.Responses;
using ProductService.Contracts.SubTypes;
using ProductService.Infrastructure.Repositories.Extensions;
using Rusty.Template.Contracts.Requests;

#endregion

namespace ProductService.Infrastructure.Repositories.Base;

public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public virtual (IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems,
        string orderBy,
        OrderDirection orderDirection,
        Expression<Func<TEntity, bool>>?
            expression)
    {
        var query = DbSet.WhereNullable(expression);

        query = query.OrderByWithDirection(orderBy, orderDirection);

        return query.PaginateWithTotalCount(skipItems, takeItems);
    }


    public virtual async Task<PagedResponse<TEntity>> PaginateAsync(
        OrderedPagedRequest request,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>,
                IQueryable<TEntity>>?
            includes = null)
    {
        var query = IncludeIfNotNull(includes);
        query = query.OrderByWithDirectionNullable(request.OrderByData);
        return await query.PaginateWithTotalCountAsListAsync(request.PageData, cancellationToken);
    }


    public virtual async Task<PagedResponse<TResult>> PaginateAsync<TResult>(
        OrderedPagedRequest request, CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>?
            includes = null) where TResult : class
    {
        var query = IncludeIfNotNull(includes);

        var totalCount = await query.CountAsync(cancellationToken);

        query = query.OrderByWithDirectionNullable(request.OrderByData);

        query = query.Paginate(request.PageData);

        return new PagedResponse<TResult>(await query.ProjectToType<TResult>().ToListAsync(cancellationToken),
            totalCount);
    }

    public virtual async Task<PagedResponse<TResult>> PaginateAsync<TResult>(
        OrderedPagedRequest request, Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) where TResult : class
    {
        var query = IncludeIfNotNull(includes);

        query = query.Where(expression);

        var totalCount = await query.CountAsync(cancellationToken);

        query = query.OrderByWithDirectionNullable(request.OrderByData);

        query = query.Paginate(request.PageData);

        return new PagedResponse<TResult>(await query.ProjectToType<TResult>().ToListAsync(cancellationToken),
            totalCount);
    }
}
