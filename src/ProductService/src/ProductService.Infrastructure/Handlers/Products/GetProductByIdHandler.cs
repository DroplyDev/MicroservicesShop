// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record GetProductByIdHandler : IActionRequestHandler<GetProductByIdRequest>
{
    private readonly IProductRepo _productRepo;

    public GetProductByIdHandler(IProductRepo productRepo)
    {
        _productRepo = productRepo;
    }

    public async ValueTask<IActionResult> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepo.GetByIdAsync(request.Id, cancellationToken);
        return new OkObjectResult(product.Adapt<ProductDto>());
    }
}
