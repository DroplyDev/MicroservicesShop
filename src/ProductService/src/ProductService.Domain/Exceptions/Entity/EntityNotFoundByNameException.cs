using Serilog.Events;

namespace ProductService.Domain.Exceptions.Entity;

public class EntityNotFoundByNameException<TEntity> : BaseEntityException where TEntity : class
{
    public EntityNotFoundByNameException(string name) : base($@"{typeof(TEntity).Name} with name {name} was not found",
        404, LogEventLevel.Warning)
    {
        Name = name;
    }


    public string Name { get; }
}