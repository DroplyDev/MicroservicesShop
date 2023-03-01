namespace ProductService.Contracts.Dtos.ProductImage;

public class ProductImageDto
{
    public int Id { get; set; }
    public byte[] Icon { get; set; } = null!;
}