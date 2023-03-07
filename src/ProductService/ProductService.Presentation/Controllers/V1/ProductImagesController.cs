using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.ProductImage;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;
using ProductService.Infrastructure.Attributes;
using ProductService.Infrastructure.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Presentation.Controllers.V1;
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0", Deprecated = false)]
public sealed class ProductImagesController : BaseApiController
{
	private readonly IProductImageRepo _productImageRepo;
	private readonly IProductRepo _productRepo;
	public ProductImagesController(IProductRepo productRepo, IProductImageRepo productImageRepo)
	{
		_productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
		_productImageRepo = productImageRepo ?? throw new ArgumentNullException(nameof(productImageRepo));
	}

	#region Images

	[SwaggerOperation(Summary = "Add thumbnail",
		Description = "Adds thumbnail for an existing product"
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[SwaggerResponse(StatusCodes.Status200OK,
		"Images retrieved successfully",
		typeof(List<ProductImageDto>)
	)]
	[HttpGet("images")]
	public async Task<IActionResult> GetProductImages(int productId, CancellationToken cancellationToken)
	{
		if (!await _productRepo.ExistsAsync(productId, cancellationToken))
			throw new EntityNotFoundByIdException<Product>(productId);
		var productImages = await _productImageRepo.Where(pi => pi.ProductId == productId)
			.ProjectToType<ProductImageDto>().ToListAsync(cancellationToken);
		return Ok(productImages);
	}

	[SwaggerOperation(Summary = "Add image for product",
		Description = "Adds image for an existing product"
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[SwaggerResponse(StatusCodes.Status204NoContent,
		"Image created successfully"
	)]
	[HttpPost("{productId:int}/images")]
	[ImageExtensionFilter]
	public async Task<IActionResult> CreateImageForProductAsync([SwaggerParameter("The product id")] int productId,
		IFormFile image)
	{
		var product = await _productRepo.FirstOrDefaultAsTrackingAsync(p => p.Id == productId) ??
					  throw new EntityNotFoundByIdException<Product>(productId);
		product.ProductImages.Add(new ProductImage
		{
			Icon = await FileManagerService.FormFileToByteArrayAsync(image)
		});
		await _productRepo.SaveChangesAsync();
		return NoContent();
	}

	[SwaggerOperation(Summary = "Delete image",
		Description = "Deletes image for a specific product"
	)]
	[SwaggerResponse(StatusCodes.Status404NotFound,
		"Image or product with id was not found",
		typeof(ApiExceptionResponse)
	)]
	[SwaggerResponse(StatusCodes.Status204NoContent,
		"Image deleted successfully"
	)]
	[HttpDelete("{productId:int}/images/{imageId:int}")]
	public async Task<IActionResult> DeleteImageForProductAsync([SwaggerParameter("The product id")] int productId,
		[SwaggerParameter("The image id")] int imageId)
	{
		var product = await _productRepo.FirstOrDefaultAsTrackingAsync(p => p.Id == productId, includes =>
						  includes.Include(i => i.ProductImages.FirstOrDefault(pi => pi.Id == imageId))) ??
					  throw new EntityNotFoundByIdException<Product>(productId);
		if (product.ProductImages.Count == 0)
			throw new EntityNotFoundByIdException<Product>(imageId);
		product.ProductImages.Clear();
		await _productRepo.SaveChangesAsync();
		return NoContent();
	}

	#endregion

	#region Thumbnails

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
	[HttpPost("{productId:int}/thumbnails")]
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
	[HttpDelete("{productId:int}/thumbnails")]
	public async Task<IActionResult> DeleteThumbnailForProductAsync([SwaggerParameter("The product id")] int productId)
	{
		var product = await _productRepo.FirstOrDefaultAsTrackingAsync(p => p.Id == productId) ??
					  throw new EntityNotFoundByIdException<Product>(productId);
		product.Thumbnail = null;
		await _productRepo.SaveChangesAsync();
		return NoContent();
	}

	#endregion
}