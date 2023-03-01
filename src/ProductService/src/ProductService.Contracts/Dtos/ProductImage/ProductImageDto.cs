namespace ProductService.Contracts.Dtos.ProductImage;

public sealed record ProductImageDto
{
	public int Id { get; set; }
	public byte[] Icon { get; set; } = null!;
}