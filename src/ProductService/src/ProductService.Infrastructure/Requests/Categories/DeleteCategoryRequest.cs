namespace ProductService.Infrastructure.Requests.Categories;

public sealed record DeleteCategoryRequest(int Id) : IActionRequest;
