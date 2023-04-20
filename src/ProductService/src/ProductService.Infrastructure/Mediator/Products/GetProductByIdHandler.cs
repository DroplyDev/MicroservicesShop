using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Caching;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;

namespace ProductService.Infrastructure.Mediator.Products;

public sealed record GetProductByIdRequest(int Id) : IActionRequest;

public sealed record GetProductByIdHandler : IActionRequestHandler<GetProductByIdRequest>
{
    private readonly ICacheService _cacheService;
    private readonly IProductRepo _productRepo;

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
