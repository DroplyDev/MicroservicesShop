using ProductService.Contracts.Requests.Pagination;

namespace ProductService.Infrastructure.Requests.Products;

public sealed record GetPagedProductsRequest(FilterOrderPageRequest Params) : IActionRequest;
