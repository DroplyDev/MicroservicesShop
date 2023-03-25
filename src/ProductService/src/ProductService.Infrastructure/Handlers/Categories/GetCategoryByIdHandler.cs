using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Categories;
using ProductService.Infrastructure.Requests.Categories;

namespace ProductService.Infrastructure.Handlers.Categories;

public sealed record GetCategoryByIdHandler : IActionRequestHandler<GetCategoryByIdRequest>
{
	private readonly ICategoryRepo _categoryRepo;

	public GetCategoryByIdHandler(ICategoryRepo categoryRepo)
	{
		_categoryRepo = categoryRepo;
	}

	public async ValueTask<IActionResult> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
	{
		var category = await _categoryRepo.GetByIdAsync(request.Id, cancellationToken);
		return new OkObjectResult(category.Adapt<CategoryDto>());
	}
}
