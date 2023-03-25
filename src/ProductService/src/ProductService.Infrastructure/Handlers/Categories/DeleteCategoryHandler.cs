// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Infrastructure.Requests.Categories;

namespace ProductService.Infrastructure.Handlers.Categories;

public sealed record DeleteCategoryHandler : IActionRequestHandler<DeleteCategoryRequest>
{
	private readonly ICategoryRepo _categoryRepo;

	public DeleteCategoryHandler(ICategoryRepo categoryRepo)
	{
		_categoryRepo = categoryRepo;
	}

	public async ValueTask<IActionResult> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
	{
		await _categoryRepo.DeleteAsync(request.Id);
		return new NoContentResult();
	}
}
