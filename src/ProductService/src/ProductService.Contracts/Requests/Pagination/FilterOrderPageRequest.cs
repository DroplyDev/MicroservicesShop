#region

using ProductService.Contracts.SubTypes;
using Rusty.Template.Contracts.Requests;

#endregion

namespace ProductService.Contracts.Requests.Pagination;

/// <summary>
///     Request with filter order by and pagination
/// </summary>
public sealed class FilterOrderPageRequest : OrderedPagedRequest
{
	/// <summary>Filter data class.</summary>
	public FilterData? FilterData { get; init; }
}