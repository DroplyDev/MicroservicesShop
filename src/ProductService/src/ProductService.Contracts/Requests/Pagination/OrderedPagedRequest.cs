// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using ProductService.Contracts.SubTypes;

#endregion

// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

/// <summary>
///     Request with order by and pagination
/// </summary>
public class OrderedPagedRequest
{
    /// <summary>Page data class.</summary>
    public PageData? PageData { get; init; }

    /// <summary>Order by data class.</summary>
    public OrderByData OrderByData { get; init; } = null!;
}
