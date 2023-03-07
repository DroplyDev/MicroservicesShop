#region

using Microsoft.EntityFrameworkCore;
using ProductService.Contracts.SubTypes;
using ProductService.Infrastructure.Repositories.Extensions;

#endregion

namespace ProductService.Infrastructure.Repositories.Base;

public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
	public virtual IQueryable<TEntity> GetAll()
	{
		return DbSet;
	}


	public virtual IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection)
	{
		return DbSet.OrderByWithDirection(orderBy, orderDirection);
	}
}