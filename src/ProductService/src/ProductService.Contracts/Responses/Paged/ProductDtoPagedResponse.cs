using ProductService.Contracts.Dtos.Products;

namespace ProductService.Contracts.Responses.Paged;

/// <summary>
///     User paged payload response
/// </summary>
public sealed record ProductDtoPagedResponse(IEnumerable<ProductDto> Data, int TotalCount)
    : PagedResponse<ProductDto>(Data, TotalCount);
