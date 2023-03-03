#region

using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;
using ProductService.Infrastructure.Attributes;
using ProductService.Infrastructure.Services;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace ProductService.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public sealed class ProductsController : BaseApiController
{
	private readonly IProductRepo _productRepo;

	public ProductsController(IProductRepo productRepo)
	{
		_productRepo = productRepo;
	}

	[SwaggerOperation(Summary = "Get paged Products",
		Description = "Returns paged list"
	)]
	[SwaggerResponse(StatusCodes.Status200OK,
		"Products retrieved successfully",
		typeof(PagedResponse<ProductDto>)
	)]
	[HttpPost("paged")]
	public async Task<IActionResult> GetFilteredPagedProductsAsync(FilterOrderPageRequest request,
		CancellationToken cancellationToken)
	{
		return Ok(await _productRepo.PaginateAsync<ProductDto>(request, cancellationToken));
	}

	[SwaggerOperation(
		Summary = "Get Product by id",
		Description = "Returns paged list"
	)]
	[SwaggerResponse(StatusCodes.Status200OK,
		"Product retrieved successfully",
		typeof(ProductDto)
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetProductByIdAsync([SwaggerParameter("The product id")] int id,
		CancellationToken cancellationToken)
	{
		var product = await _productRepo.GetByIdAsync(id, cancellationToken) ??
					  throw new EntityNotFoundByIdException<Product>(id);
		return Ok(product.Adapt<ProductDto>());
	}

	[SwaggerOperation(Summary = "Get Product to update by id",
		Description = "Returns Product dto for update"
	)]
	[SwaggerResponse(StatusCodes.Status200OK,
		"Product retrieved successfully",
		typeof(ProductUpdateDto)
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[HttpGet("update/{id:int}")]
	public async Task<IActionResult> GetProductToUpdateByIdAsync([SwaggerParameter("The product id")] int id,
		CancellationToken cancellationToken)
	{
		var product = await _productRepo.GetByIdAsync(id, cancellationToken) ??
					  throw new EntityNotFoundByIdException<Product>(id);
		return Ok(product.Adapt<ProductUpdateDto>());
	}

	[SwaggerOperation(Summary = "Create new Product",
		Description = "Creates new Product"
	)]
	[SwaggerResponse(
		StatusCodes.Status201Created, "Product created successfully",
		typeof(ProductDto)
	)]
	[HttpPost]
	public async Task<IActionResult> CreateProductAsync(ProductCreateDto dto)
	{
		var product = await _productRepo.CreateAsync(dto.Adapt<Product>());
		return CreatedAtAction("GetProductById", new { id = product.Id }, product.Adapt<ProductDto>());
	}

	[SwaggerOperation(Summary = "Update Product",
		Description = "Updates existing Product"
	)]
	[SwaggerResponse(
		StatusCodes.Status204NoContent, "Product updated successfully"
	)]
	[HttpPut("{id:int}")]
	[HttpPutIdCompare]
	public async Task<IActionResult> UpdateProductAsync([SwaggerParameter("The product id")] int id,
		ProductUpdateDto dto)
	{
		var product = await _productRepo.FirstOrDefaultAsTrackingAsync(u => u.Id == id) ??
					  throw new EntityNotFoundByIdException<Product>(id);
		dto.Adapt(product);
		await _productRepo.SaveChangesAsync();
		return NoContent();
	}

	[SwaggerOperation(Summary = "Delete Product",
		Description = "Deletes existing Product"
	)]
	[SwaggerResponse(StatusCodes.Status204NoContent,
		"Product deleted successfully"
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteProductAsync([SwaggerParameter("The product id")] int id)
	{
		await _productRepo.DeleteAsync(id);
		return NoContent();
	}
	[SwaggerOperation(Summary = "Add thumbnail",
		Description = "Adds thumbnail for an existing product"
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[SwaggerResponse(StatusCodes.Status204NoContent,
		"Thumbnail updated successfully"
	)]
	[HttpPost("{productId:int}/thumbnail")]
	[ImageExtensionFilter]
	public async Task<IActionResult> CreateThumbnailForProductAsync([SwaggerParameter("The product id")] int productId,
		IFormFile image)
	{
		var product = await _productRepo.FirstOrDefaultAsTrackingAsync(p => p.Id == productId) ??
					  throw new EntityNotFoundByIdException<Product>(productId);
		product.Thumbnail = await FileManagerService.FormFileToByteArrayAsync(image);
		await _productRepo.SaveChangesAsync();
		return NoContent();
	}

	[SwaggerOperation(Summary = "Delete thumbnail",
		Description = "Deletes thumbnail for a specific product"
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[SwaggerResponse(StatusCodes.Status204NoContent,
		"Thumbnail deleted successfully"
	)]
	[HttpDelete("{productId:int}/thumbnail")]
	public async Task<IActionResult> DeleteThumbnailForProductAsync([SwaggerParameter("The product id")] int productId)
	{
		var product = await _productRepo.FirstOrDefaultAsTrackingAsync(p => p.Id == productId) ??
					  throw new EntityNotFoundByIdException<Product>(productId);
		product.Thumbnail = null;
		await _productRepo.SaveChangesAsync();
		return NoContent();
	}
}