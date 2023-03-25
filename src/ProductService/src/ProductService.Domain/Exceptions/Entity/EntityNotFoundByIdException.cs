// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
