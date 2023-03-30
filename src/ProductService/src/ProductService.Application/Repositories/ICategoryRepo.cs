// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProductService.Application.Repositories.BaseRepo;
using ProductService.Domain;

namespace ProductService.Application.Repositories;

public interface ICategoryRepo : IBaseGenericRepo<Category>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
