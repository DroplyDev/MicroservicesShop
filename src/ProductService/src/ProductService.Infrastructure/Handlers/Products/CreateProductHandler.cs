// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record CreateProductHandler : IActionRequestHandler<CreateProductRequest>
{
	private readonly IProductRepo _productRepo;
	private readonly IValidator<ProductCreateDto> _validator;

	public CreateProductHandler(IProductRepo productRepo, IValidator<ProductCreateDto> validator)
	{
		_productRepo = productRepo;
		_validator = validator;
	}

	public async ValueTask<IActionResult> Handle(CreateProductRequest request, CancellationToken cancellationToken)
	{
		var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
		if (!validationResult.IsValid)
		{
			return new BadRequestObjectResult(BadRequestResponse.With(validationResult));
		}

		var product = await _productRepo.CreateAsync(request.Dto.Adapt<Product>());
		return new CreatedAtRouteResult("GetProductById", new { id = product.Id }, product.Adapt<ProductDto>());
	}
}
