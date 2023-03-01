#region

using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ProductService.Infrastructure.Repositories.Base;

public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
	public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return IncludeIfNotNull(includes).FirstOrDefault(expression);
	}

	public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken = default,
		Func<IQueryable<TEntity>,
			IQueryable<TEntity>>? includes = null)
	{
		return await IncludeIfNotNull(includes).FirstOrDefaultAsync(expression, cancellationToken);
	}
	public async Task<TEntity> FirstOrDefaultAsTrackingAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return await IncludeIfNotNull(includes).AsTracking().FirstAsync(expression);
	}

	public virtual TEntity First(Expression<Func<TEntity, bool>> expression,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return IncludeIfNotNull(includes).First(expression);
	}

	public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken = default,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return await IncludeIfNotNull(includes).FirstAsync(expression, cancellationToken);
	}
	public async Task<TEntity> FirstAsTrackingAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return await IncludeIfNotNull(includes).AsTracking().FirstAsync(expression);
	}
}