// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record GetProductToUpdateByIdHandler : IActionRequestHandler<GetProductToUpdateByIdRequest>
{
	private readonly IProductRepo _productRepo;

	public GetProductToUpdateByIdHandler(IProductRepo productRepo)
	{
		_productRepo = productRepo;
	}

	public async ValueTask<IActionResult> Handle(GetProductToUpdateByIdRequest request,
		CancellationToken cancellationToken)
	{
		var product = await _productRepo.GetByIdAsync(request.Id, cancellationToken);
		return new OkObjectResult(product.Adapt<ProductUpdateDto>());
	}
}
