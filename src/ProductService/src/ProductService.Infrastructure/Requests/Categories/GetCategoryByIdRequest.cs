namespace ProductService.Infrastructure.Requests.Categories;

public sealed record GetCategoryByIdRequest(int Id) : IActionRequest;
