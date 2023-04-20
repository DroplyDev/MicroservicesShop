using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;

namespace ProductService.Infrastructure.Mediator.Products;

public sealed record DeleteProductRequest(int Id) : IActionRequest;

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
