using ProductService.Contracts.SubTypes;
using Rusty.Template.Contracts.Requests;

namespace ProductService.Contracts.Requests.Pagination;

/// <summary>
///     Request with filter order by and pagination
/// </summary>
public sealed class FilterOrderPageRequest : OrderedPagedRequest
{
    /// <summary>Filter data class.</summary>
    public FilterByDateOptions? FilterData { get; init; }
}
