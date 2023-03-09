#region

using ProductService.Contracts.Dtos.Products;

#endregion

namespace ProductService.Contracts.Responses.Paged;

/// <summary>
///     User paged payload response
/// </summary>
public sealed record ProductDtoPagedResponse(IEnumerable<ProductDto> Data, int TotalCount)
	: PagedResponse<ProductDto>(Data, TotalCount);