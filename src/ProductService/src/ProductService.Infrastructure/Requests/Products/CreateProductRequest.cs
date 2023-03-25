using ProductService.Contracts.Dtos.Products;

namespace ProductService.Infrastructure.Requests.Products;

public sealed record CreateProductRequest(ProductCreateDto Dto) : IActionRequest;
