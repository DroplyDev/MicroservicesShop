namespace ProductService.Contracts.Dtos.Products;

public class ProductUpdateDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public byte[]? Thumbnail { get; set; }

    public int CategoryId { get; set; }
}