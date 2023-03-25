using ProductService.Contracts.Requests.Pagination;

namespace ProductService.Infrastructure.Requests.Categories;

public sealed record GetPagedCategoriesRequest(FilterOrderPageRequest Params) : IActionRequest;
