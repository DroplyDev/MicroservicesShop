using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Caching;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;

namespace ProductService.Infrastructure.Mediator.Products;

public sealed record GetPagedProductsRequest(FilterOrderPageRequest Params) : IActionRequest;

public sealed record GetPagedProductsHandler : IActionRequestHandler<GetPagedProductsRequest>
{
    private readonly ICacheService _cacheService;
    private readonly IProductRepo _productRepo;
    private readonly IValidator<FilterOrderPageRequest> _validator;

    public GetPagedProductsHandler(IProductRepo productRepo, IValidator<FilterOrderPageRequest> validator,
        ICacheService cacheService)
    {
        _productRepo = productRepo;
        _validator = validator;
        _cacheService = cacheService;
    }

    public async ValueTask<IActionResult> Handle(GetPagedProductsRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.Params, cancellationToken);

        var cacheKey =
            $"{nameof(Product)}_{request.Params.FilterData?.DateFrom}_{request.Params.FilterData?.DateTo}{request.Params.PageData?.Offset}_{request.Params.PageData?.Limit}_{request.Params.OrderByData?.OrderBy}_{request.Params.OrderByData?.OrderDirection}";
        var data = await _cacheService.GetAsync<PagedResponse<ProductDto>>(cacheKey);
        if (data is null)
        {
            data = await _productRepo.PaginateAsync<ProductDto>(request.Params, cancellationToken);
            await _cacheService.SetAsync(cacheKey, data);
        }

        return new OkObjectResult(await _productRepo.PaginateAsync<ProductDto>(request.Params, cancellationToken));
    }
}
