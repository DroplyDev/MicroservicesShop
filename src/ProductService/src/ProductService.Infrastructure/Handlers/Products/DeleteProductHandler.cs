// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record DeleteProductHandler : IActionRequestHandler<DeleteProductRequest>
{
	private readonly IProductRepo _productRepo;

	public DeleteProductHandler(IProductRepo productRepo)
	{
		_productRepo = productRepo;
	}

	public async ValueTask<IActionResult> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
	{
		await _productRepo.DeleteAsync(request.Id);
		return new NoContentResult();
	}
}
