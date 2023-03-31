// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ProductService.Application.Caching;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Domain;
using ProductService.Infrastructure.Extensions;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record GetProductByIdHandler : IActionRequestHandler<GetProductByIdRequest>
{
    private readonly IProductRepo _productRepo;
    private readonly ICacheService _cacheService;

    public GetProductByIdHandler(IProductRepo productRepo, ICacheService cacheService)
    {
        _productRepo = productRepo;
        _cacheService = cacheService;
    }

    public async ValueTask<IActionResult> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepo.GetByIdAsync(request.Id, cancellationToken);
        return new OkObjectResult(product.Adapt<ProductDto>());
    }
}
