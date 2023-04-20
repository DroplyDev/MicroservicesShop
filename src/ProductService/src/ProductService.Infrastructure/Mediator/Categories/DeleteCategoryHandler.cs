using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;

namespace ProductService.Infrastructure.Mediator.Categories;

public sealed record DeleteCategoryRequest(int Id) : IActionRequest;

public sealed record DeleteCategoryHandler : IActionRequestHandler<DeleteCategoryRequest>
{
    private readonly ICategoryRepo _categoryRepo;

    public DeleteCategoryHandler(ICategoryRepo categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async ValueTask<IActionResult> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        await _categoryRepo.DeleteAsync(request.Id);
        return new NoContentResult();
    }
}
