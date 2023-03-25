// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Infrastructure.Requests.Categories;

namespace ProductService.Infrastructure.Handlers.Categories;

public sealed record GetPagedCategoriesHandler : IActionRequestHandler<GetPagedCategoriesRequest>
{
	private readonly ICategoryRepo _categoryRepo;
	private readonly IValidator<FilterOrderPageRequest> _validator;

	public GetPagedCategoriesHandler(ICategoryRepo categoryRepo, IValidator<FilterOrderPageRequest> validator)
	{
		_categoryRepo = categoryRepo;
		_validator = validator;
	}

	public async ValueTask<IActionResult> Handle(GetPagedCategoriesRequest request, CancellationToken cancellationToken)
	{
		var validationResult = await _validator.ValidateAsync(request.Params, cancellationToken);
		if (!validationResult.IsValid)
		{
			return new BadRequestObjectResult(BadRequestResponse.With(validationResult));
		}

		return new OkObjectResult(await _categoryRepo.PaginateAsync<ProductDto>(request.Params, cancellationToken));
	}
}
