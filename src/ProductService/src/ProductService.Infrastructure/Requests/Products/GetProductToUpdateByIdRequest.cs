namespace ProductService.Infrastructure.Requests.Products;

public sealed record GetProductToUpdateByIdRequest(int Id) : IActionRequest;
