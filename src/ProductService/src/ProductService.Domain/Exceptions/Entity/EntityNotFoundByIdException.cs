using Serilog.Events;

namespace ProductService.Domain.Exceptions.Entity;

public class EntityNotFoundByIdException<TEntity> : BaseEntityException where TEntity : class
{
	public EntityNotFoundByIdException(object id) : base(
		$@"{typeof(TEntity).Name} with id {id} was not found", 404, LogEventLevel.Warning)
	{
		Id = id;
	}


	public object Id { get; }
}