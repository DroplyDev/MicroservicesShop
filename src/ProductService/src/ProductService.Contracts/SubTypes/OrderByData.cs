// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     Order data subtype
/// </summary>
public sealed class OrderByData
{
    /// <summary>Order property name.</summary>
    /// <example>FieldName</example>

    public string OrderBy { get; set; } = null!;

    /// <summary>Order direction enum.</summary>
    public OrderDirection OrderDirection { get; set; }
}
