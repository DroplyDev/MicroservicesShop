using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;

namespace ProductService.Infrastructure.Mediator.Products;

public sealed record GetProductToUpdateByIdRequest(int Id) : IActionRequest;

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
