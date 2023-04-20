using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Categories;

namespace ProductService.Infrastructure.Mediator.Categories;

public sealed record GetCategoryToUpdateByIdRequest(int Id) : IActionRequest;

public sealed record GetCategoryToUpdateByIdHandler : IActionRequestHandler<GetCategoryToUpdateByIdRequest>
{
	private readonly ICategoryRepo _categoryRepo;

	public GetCategoryToUpdateByIdHandler(ICategoryRepo categoryRepo)
	{
		_categoryRepo = categoryRepo;
	}

	public async ValueTask<IActionResult> Handle(GetCategoryToUpdateByIdRequest request,
		CancellationToken cancellationToken)
	{
		var category = await _categoryRepo.GetByIdAsync(request.Id, cancellationToken);
		return new OkObjectResult(category.Adapt<CategoryUpdateDto>());
	}
}
