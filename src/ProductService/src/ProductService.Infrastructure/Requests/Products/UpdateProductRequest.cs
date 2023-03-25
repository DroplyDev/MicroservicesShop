using ProductService.Contracts.Dtos.Products;

namespace ProductService.Infrastructure.Requests.Products;

public sealed record UpdateProductRequest(int Id, ProductUpdateDto Dto) : IActionRequest;
