#region

using System.Linq.Expressions;
using ProductService.Contracts.SubTypes;
using ProductService.Domain.Exceptions.Entity;

#endregion

namespace ProductService.Infrastructure.Repositories.Extensions;

public static class OrderByExtensions
{
	public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
		this IQueryable<TEntity> query, string propertyName, OrderDirection orderDirection) where TEntity : class
	{
		var entityType = typeof(TEntity);
		//Create x=>x.PropName
		var propertyInfo = entityType.GetProperty(propertyName)
		                   ?? throw new EntityOrderParamNameNotValidException<TEntity>(propertyName);

		var arg = Expression.Parameter(entityType, "x");
		var property = Expression.Property(arg, propertyName);

		var selector = Expression.Lambda(property, arg);
		//Get System.Linq.Queryable.OrderByDescending() method.
		var enumerableType = typeof(Queryable);
		var method = enumerableType.GetMethods()
			.Where(m =>
				m.Name ==
				(orderDirection == OrderDirection.Asc ? "OrderBy" : "OrderByDescending") &&
				m.IsGenericMethodDefinition)
			.Single(m =>
			{
				var parameters = m.GetParameters().ToList();
				//Put more restriction here to ensure selecting the right overload                
				return parameters.Count == 2; //overload that has 2 parameters
			});
		//The linq's OrderByDescending<TEntity, TKey> has two generic types, which provided here
		var genericMethod = method
			.MakeGenericMethod(entityType, propertyInfo?.PropertyType!);

		/*Call query.OrderBy(selector), with query and selector: x=> x.PropName
		  Note that we pass the selector as Expression to the method and we don't compile it.
		  By doing so EF can extract "order by" columns and generate SQL for it.*/
		var newQuery = (IOrderedQueryable<TEntity>) genericMethod
			.Invoke(genericMethod, new object[] {query, selector})!;
		return newQuery;
	}


	public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
		this IQueryable<TEntity> query, OrderByData orderByData) where TEntity : class
	{
		return query.OrderByWithDirection(orderByData.OrderBy, orderByData.OrderDirection);
	}

	public static IQueryable<TEntity> OrderByWithDirectionNullable<TEntity>(
		this IQueryable<TEntity> query, OrderByData? orderByData) where TEntity : class
	{
		return orderByData is null
			? query
			: query.OrderByWithDirection(orderByData.OrderBy, orderByData.OrderDirection);
	}

	public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
		this IQueryable<TEntity> query, Expression<Func<TEntity, object>> keySelector, OrderDirection orderDirection)
	{
		return orderDirection == OrderDirection.Asc ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
	}

	public static IOrderedQueryable<TEntity> OrderBy<TEntity>(
		this IQueryable<TEntity> query, string orderBy) where TEntity : class
	{
		return query.OrderByWithDirection(orderBy, OrderDirection.Asc);
	}


	public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(
		this IQueryable<TEntity> query, string orderBy) where TEntity : class
	{
		return query.OrderByWithDirection(orderBy, OrderDirection.Desc);
	}
}