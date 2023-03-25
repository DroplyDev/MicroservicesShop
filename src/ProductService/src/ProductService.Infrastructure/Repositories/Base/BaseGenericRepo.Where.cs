#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductService.Contracts.SubTypes;
using ProductService.Infrastructure.Repositories.Extensions;

#endregion

namespace ProductService.Infrastructure.Repositories.Base;

public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression);
    }


    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
        OrderDirection orderDirection)
    {
        return DbSet
            .Where(expression)
            .OrderByWithDirection(orderBy, orderDirection);
    }
}
