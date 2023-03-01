namespace ProductService.Contracts.Dtos.Categories;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Icon { get; set; }
}