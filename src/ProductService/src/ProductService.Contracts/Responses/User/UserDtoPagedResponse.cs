#region

using ProductService.Contracts.Dtos.Products;

#endregion

namespace ProductService.Contracts.Responses.User;

/// <summary>
///     User paged payload response
/// </summary>
public sealed record UserDtoPagedResponse(IEnumerable<ProductDto> Data, int TotalCount)
    : PagedResponse<ProductDto>(Data, TotalCount);