#region

using ProductService.Infrastructure.Database;
using ProductService.Infrastructure.Repositories.Base;

#endregion

namespace ProductService.Infrastructure.Repositories;

public abstract class AppDbGenericRepo<TEntity> : BaseGenericRepo<AppDbContext, TEntity> where TEntity : class
{
    protected AppDbGenericRepo(AppDbContext context) : base(context)
    {
    }
}
