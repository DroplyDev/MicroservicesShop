namespace ProductService.Infrastructure.Requests.Products;

public sealed record GetProductByIdRequest(int Id) : IActionRequest;
