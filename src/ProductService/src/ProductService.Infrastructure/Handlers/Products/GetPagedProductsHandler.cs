// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record GetPagedProductsHandler : IActionRequestHandler<GetPagedProductsRequest>
{
    private readonly IProductRepo _productRepo;
    private readonly IValidator<FilterOrderPageRequest> _validator;

    public GetPagedProductsHandler(IProductRepo productRepo, IValidator<FilterOrderPageRequest> validator)
    {
        _productRepo = productRepo;
        _validator = validator;
    }

    public async ValueTask<IActionResult> Handle(GetPagedProductsRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.Params, cancellationToken);


        return new OkObjectResult(await _productRepo.PaginateAsync<ProductDto>(request.Params, cancellationToken));
    }
}
