using ProductService.Contracts.Dtos.Categories;

namespace ProductService.Infrastructure.Requests.Categories;

public sealed record CreateCategoryRequest(CategoryCreateDto Dto) : IActionRequest;
