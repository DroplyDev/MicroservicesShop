using Mediator;
using Microsoft.AspNetCore.Mvc;
using ProductService.Contracts.Dtos.Categories;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Infrastructure.Requests.Categories;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public class CategoriesController : BaseRoutedController
{
	private readonly IMediator _mediator;

	public CategoriesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[SwaggerOperation(
		Summary = "Get paged Categories",
		Description = "Returns paged list"
	)]
	[SwaggerResponse(
		StatusCodes.Status200OK,
		"Categories retrieved successfully",
		typeof(PagedResponse<CategoryDto>)
	)]
	[HttpPost("paged")]
	public async Task<IActionResult> GetFilteredPagedCategoriesAsync(FilterOrderPageRequest request,
		CancellationToken cancellationToken)
	{
		return await _mediator.Send(new GetPagedCategoriesRequest(request), cancellationToken);
	}

	[SwaggerOperation(
		Summary = "Get Category by id",
		Description = "Returns paged list"
	)]
	[SwaggerResponse(
		StatusCodes.Status200OK,
		"Category retrieved successfully",
		typeof(CategoryDto)
	)]
	[SwaggerResponse(
		StatusCodes.Status404NotFound,
		"Category with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetCategoryByIdAsync([SwaggerParameter("The category id")] int id,
		CancellationToken cancellationToken)
	{
		return await _mediator.Send(new GetCategoryByIdRequest(id), cancellationToken);
	}

	[SwaggerOperation(
		Summary = "Get Category to update by id",
		Description = "Returns Category dto for update"
	)]
	[SwaggerResponse(
		StatusCodes.Status200OK,
		"Category retrieved successfully",
		typeof(CategoryUpdateDto)
	)]
	[SwaggerResponse(
		StatusCodes.Status404NotFound,
		"Category with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[HttpGet("update/{id:int}")]
	public async Task<IActionResult> GetCategoryToUpdateByIdAsync([SwaggerParameter("The category id")] int id,
		CancellationToken cancellationToken)
	{
		return await _mediator.Send(new GetCategoryToUpdateByIdRequest(id), cancellationToken);
	}

	[SwaggerOperation(
		Summary = "Create new Category",
		Description = "Creates new Category"
	)]
	[SwaggerResponse(
		StatusCodes.Status201Created, "Category created successfully",
		typeof(CategoryDto)
	)]
	[HttpPost]
	public async Task<IActionResult> CreateCategoryAsync(CategoryCreateDto dto)
	{
		return await _mediator.Send(new CreateCategoryRequest(dto));
	}

	[SwaggerOperation(
		Summary = "Update Category",
		Description = "Updates existing Category"
	)]
	[SwaggerResponse(
		StatusCodes.Status204NoContent, "Category updated successfully"
	)]
	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateCategoryAsync([SwaggerParameter("The category id")] int id,
		CategoryUpdateDto dto)
	{
		return await _mediator.Send(new UpdateCategoryRequest(id, dto));
	}

	[SwaggerOperation(
		Summary = "Delete Category",
		Description = "Deletes existing Category"
	)]
	[SwaggerResponse(
		StatusCodes.Status204NoContent, "Category deleted successfully"
	)]
	[SwaggerResponse(
		StatusCodes.Status404NotFound,
		"Category with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteCategoryAsync([SwaggerParameter("The category id")] int id)
	{
		return await _mediator.Send(new DeleteCategoryRequest(id));
	}
}
