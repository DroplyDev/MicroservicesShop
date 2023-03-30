#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProductService.Application.Repositories.BaseRepo;
using ProductService.Domain.Exceptions.Entity;

#endregion

namespace ProductService.Infrastructure.Repositories.Base;

public abstract partial class BaseGenericRepo<TContext, TEntity> : IBaseGenericRepo<TEntity>
    where TEntity : class where TContext : DbContext
{
    protected readonly TContext Context;


    protected readonly DbSet<TEntity> DbSet;


    protected BaseGenericRepo(TContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }


    public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(new[] { id }, cancellationToken) ??
               throw new EntityNotFoundByIdException<TEntity>(id);
    }

    public async Task<TEntity?> GetByIdOrDefaultAsync(object id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(new[] { id }, cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await CreateNoSaveAsync(entity);
        await SaveChangesAsync();
        return entity;
    }


    public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var rangeAsync = entities.ToList();
        await DbSet.AddRangeAsync(rangeAsync);
        await SaveChangesAsync();
        return rangeAsync;
    }


    public virtual async Task CreateNoSaveAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public Task ExecuteUpdateAsync(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> entities)
    {
        return DbSet.ExecuteUpdateAsync(entities);
    }

    public Task UpdateAsync(TEntity entity)
    {
        UpdateNoSave(entity);
        return SaveChangesAsync();
    }


    public virtual void UpdateNoSave(TEntity entity)
    {
        DbSet.Update(entity);
    }


    public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        UpdateRangeNoSave(entities);
        return SaveChangesAsync();
    }


    public virtual void UpdateRangeNoSave(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }


    public virtual void Attach(TEntity entity)
    {
        DbSet.Attach(entity);
    }

    public virtual void AttachRange(IEnumerable<TEntity> entities)
    {
        DbSet.AttachRange(entities);
    }

    public Task DeleteAsync(TEntity entity)
    {
        DeleteNoSave(entity);
        return SaveChangesAsync();
    }


    public async Task DeleteAsync(object id)
    {
        var entity = await GetByIdAsync(id, CancellationToken.None)
                     ?? throw new EntityNotFoundByIdException<TEntity>(id);

        DeleteNoSave(entity);
        await SaveChangesAsync();
    }


    public virtual void DeleteNoSave(TEntity entity)
    {
        DbSet.Remove(entity);
    }


    public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        DeleteNoSaveRange(entities);
        return SaveChangesAsync();
    }


    public virtual void DeleteNoSaveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }


    public virtual async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
    {
        return await GetByIdOrDefaultAsync(id, cancellationToken) is not null;
    }


    public virtual Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(item => item.Equals(entity), cancellationToken);
    }


    public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(expression, cancellationToken);
    }


    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
    {
        return !await DbSet.AnyAsync(cancellationToken);
    }


    public async Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return !await DbSet.AnyAsync(expression, cancellationToken);
    }


    public Task<int> SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }


    protected IQueryable<TEntity> IncludeIfNotNull(
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
    {
        return includes is null ? DbSet : includes(DbSet);
    }
}
