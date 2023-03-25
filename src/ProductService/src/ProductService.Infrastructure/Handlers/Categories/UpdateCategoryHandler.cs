// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Categories;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;
using ProductService.Infrastructure.Requests.Categories;

namespace ProductService.Infrastructure.Handlers.Categories;

public sealed record UpdateCategoryHandler : IActionRequestHandler<UpdateCategoryRequest>
{
	private readonly ICategoryRepo _categoryRepo;
	private readonly IValidator<CategoryUpdateDto> _validator;

	public UpdateCategoryHandler(ICategoryRepo categoryRepo, IValidator<CategoryUpdateDto> validator)
	{
		_categoryRepo = categoryRepo;
		_validator = validator;
	}

	public async ValueTask<IActionResult> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
	{
		var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
		if (!validationResult.IsValid)
		{
			return new BadRequestObjectResult(BadRequestResponse.With(validationResult));
		}

		var product = await _categoryRepo.FirstOrDefaultAsTrackingAsync(u => u.Id == request.Id) ??
					  throw new EntityNotFoundByIdException<Category>(request.Id);
		request.Dto.Adapt(product);
		await _categoryRepo.SaveChangesAsync();
		return new NoContentResult();
	}
}
