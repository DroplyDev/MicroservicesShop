#region

using ProductService.Infrastructure.Database;
using ProductService.Infrastructure.Repositories.Base;

#endregion

namespace ProductService.Infrastructure.Repositories;

public abstract class AppDbRepo<TEntity> : BaseGenericRepo<AppDbContext, TEntity> where TEntity : class
{
    protected AppDbRepo(AppDbContext context) : base(context)
    {
    }
}