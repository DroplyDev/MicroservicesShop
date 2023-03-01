using Microsoft.AspNetCore.Http;
using ProductService.Contracts.Dtos.ProductImage;

namespace ProductService.Contracts.Dtos.Products;

public sealed class ProductCreateDto
{
	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public int Quantity { get; set; }

	public decimal Price { get; set; }
	public int CategoryId { get; set; }
}