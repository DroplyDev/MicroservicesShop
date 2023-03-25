// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Serilog.Events;

namespace ProductService.Domain.Exceptions.Entity;

public class EntityWitNameAlreadyExistsException<TEntity> : BaseEntityException where TEntity : class
{
    public EntityWitNameAlreadyExistsException(string name) : base(
        $@"{typeof(TEntity).Name} with name: {name} already exists", 409, LogEventLevel.Warning)
    {
        Name = name;
    }


    public string Name { get; }
}
