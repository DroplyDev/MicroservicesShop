// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
    public FilterByDateOptions? FilterData { get; init; }
}
