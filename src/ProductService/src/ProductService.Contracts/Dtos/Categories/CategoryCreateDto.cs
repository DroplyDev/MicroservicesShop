using Microsoft.AspNetCore.Http;

namespace ProductService.Contracts.Dtos.Categories;

public sealed class CategoryCreateDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public IFormFile? ThumbnailImage { get; set; }

    public byte[]? Icon { get; set; }
}
