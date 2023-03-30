// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Cache;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record GetPagedProductsHandler : IActionRequestHandler<GetPagedProductsRequest>
{
    private readonly IProductRepo _productRepo;
    private readonly IValidator<FilterOrderPageRequest> _validator;
    private readonly ICacheService _cacheService;

    public GetPagedProductsHandler(IProductRepo productRepo, IValidator<FilterOrderPageRequest> validator, ICacheService cacheService)
    {
        _productRepo = productRepo;
        _validator = validator;
        _cacheService = cacheService;
    }

    public async ValueTask<IActionResult> Handle(GetPagedProductsRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.Params, cancellationToken);

        var cacheKey = $"{nameof(Product)}_{request.Params.FilterData?.DateFrom}_{request.Params.FilterData?.DateTo}{request.Params.PageData?.Offset}_{request.Params.PageData?.Limit}_{request.Params.OrderByData?.OrderBy}_{request.Params.OrderByData?.OrderDirection}";
        var data = await _cacheService.GetAsync<PagedResponse<ProductDto>>(cacheKey);
        if (data is null)
        {
            data = await _productRepo.PaginateAsync<ProductDto>(request.Params, cancellationToken);
            await _cacheService.SetAsync(cacheKey, data);
        }
        return new OkObjectResult(await _productRepo.PaginateAsync<ProductDto>(request.Params, cancellationToken));
    }
}
