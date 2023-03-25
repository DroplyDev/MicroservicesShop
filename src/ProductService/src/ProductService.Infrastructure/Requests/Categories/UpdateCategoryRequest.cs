using ProductService.Contracts.Dtos.Categories;

namespace ProductService.Infrastructure.Requests.Categories;

public sealed record UpdateCategoryRequest(int Id, CategoryUpdateDto Dto) : IActionRequest;
