namespace ProductService.Infrastructure.Requests.Categories;

public sealed record GetCategoryToUpdateByIdRequest(int Id) : IActionRequest;
