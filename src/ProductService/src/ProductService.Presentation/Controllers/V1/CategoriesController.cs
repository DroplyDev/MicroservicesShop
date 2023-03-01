using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Categories;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public class CategoriesController : BaseApiController
{
	private readonly ICategoryRepo _productRepo;

	public CategoriesController(ICategoryRepo productRepo)
	{
		_productRepo = productRepo;
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
		return Ok(await _productRepo.PaginateAsync<CategoryDto>(request, cancellationToken));
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
	public async Task<IActionResult> GetCategoryByIdAsync([SwaggerParameter("The category id")] int id, CancellationToken cancellationToken)
	{
		var Category = await _productRepo.GetByIdAsync(id, cancellationToken) ??
					   throw new EntityNotFoundByIdException<Category>(id);
		return Ok(Category.Adapt<CategoryDto>());
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
	public async Task<IActionResult> GetCategoryToUpdateByIdAsync([SwaggerParameter("The category id")] int id, CancellationToken cancellationToken)
	{
		var category = await _productRepo.GetByIdAsync(id, cancellationToken) ??
					   throw new EntityNotFoundByIdException<Category>(id);
		return Ok(category.Adapt<CategoryUpdateDto>());
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
		var category = await _productRepo.CreateAsync(dto.Adapt<Category>());
		return CreatedAtAction("GetCategoryById", new { id = category.Id }, category.Adapt<CategoryDto>());
	}

	[SwaggerOperation(
		Summary = "Update Category",
		Description = "Updates existing Category"
	)]
	[SwaggerResponse(
		StatusCodes.Status204NoContent, "Category updated successfully"
	)]
	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateCategoryAsync([SwaggerParameter("The category id")] int id, CategoryUpdateDto dto)
	{
		var category =
			await _productRepo.FirstOrDefaultAsync(u => u.Id == id, default, includes => includes.AsTracking()) ??
			throw new EntityNotFoundByIdException<Category>(id);
		dto.Adapt(category);
		await _productRepo.SaveChangesAsync();
		return NoContent();
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
		await _productRepo.DeleteAsync(id);
		return NoContent();
	}
}