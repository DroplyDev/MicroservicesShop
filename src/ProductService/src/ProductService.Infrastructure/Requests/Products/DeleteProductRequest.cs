namespace ProductService.Infrastructure.Requests.Products;

public sealed record DeleteProductRequest(int Id) : IActionRequest;
