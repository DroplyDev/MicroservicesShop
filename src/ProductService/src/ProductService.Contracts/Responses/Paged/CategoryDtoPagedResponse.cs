using ProductService.Contracts.Dtos.Products;

namespace ProductService.Contracts.Responses.Paged;

public sealed record CategoryDtoPagedResponse(IEnumerable<ProductDto> Data, int TotalCount)
	: PagedResponse<ProductDto>(Data, TotalCount);