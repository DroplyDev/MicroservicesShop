using Mapster;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Repositories;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Infrastructure.Database;
using ProductService.Infrastructure.Repositories.Extensions;

namespace ProductService.Infrastructure.Repositories.Specific;

public sealed class ProductRepo : AppDbRepo<Product>, IProductRepo
{
    public ProductRepo(AppDbContext context) : base(context)
    {
    }

    public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await FirstOrDefaultAsync(item => item.Name == name, cancellationToken);
    }

    /// <summary>Paginates the asynchronous.</summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<PagedResponse<TResult>> PaginateAsync<TResult>(FilterOrderPageRequest request,
        CancellationToken cancellationToken = default) where TResult : class
    {
        IQueryable<Product> query = DbSet;

        if (request.FilterData != null)
        {
            query = query.Where(p =>
                p.CreateDate >= request.FilterData.DateFrom && p.CreateDate <= request.FilterData.DateTo);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        query = query.OrderByWithDirectionNullable(request.OrderByData);

        query = query.Paginate(request.PageData);

        return new PagedResponse<TResult>(await query.ProjectToType<TResult>().ToListAsync(cancellationToken),
            totalCount);
    }
}
