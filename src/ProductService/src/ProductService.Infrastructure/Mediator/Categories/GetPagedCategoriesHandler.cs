using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Caching;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Categories;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;

namespace ProductService.Infrastructure.Mediator.Categories;

public sealed record GetPagedCategoriesRequest(FilterOrderPageRequest Params) : IActionRequest;

public sealed record GetPagedCategoriesHandler : IActionRequestHandler<GetPagedCategoriesRequest>
{
	private readonly ICacheService _cacheService;
	private readonly ICategoryRepo _categoryRepo;
	private readonly IValidator<FilterOrderPageRequest> _validator;

	public GetPagedCategoriesHandler(ICategoryRepo categoryRepo, IValidator<FilterOrderPageRequest> validator,
		ICacheService cacheService)
	{
		_categoryRepo = categoryRepo;
		_validator = validator;
		_cacheService = cacheService;
	}

	public async ValueTask<IActionResult> Handle(GetPagedCategoriesRequest request, CancellationToken cancellationToken)
	{
		await _validator.ValidateAndThrowAsync(request.Params, cancellationToken);

		var cacheKey =
			$"{nameof(Category)}_{request.Params.FilterData?.DateFrom}_{request.Params.FilterData?.DateTo}{request.Params.PageData?.Offset}_{request.Params.PageData?.Limit}_{request.Params.OrderByData?.OrderBy}_{request.Params.OrderByData?.OrderDirection}";
		var data = await _cacheService.GetAsync<PagedResponse<CategoryDto>>(cacheKey);
		if (data is null)
		{
			data = await _categoryRepo.PaginateAsync<CategoryDto>(request.Params, cancellationToken);
			await _cacheService.SetAsync(cacheKey, data);
		}

		return new OkObjectResult(data);
	}
}
