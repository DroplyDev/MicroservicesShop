// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     Filter data subtype
/// </summary>
public sealed class FilterByDateOptions
{
    /// <summary>Start date filter.</summary>
    /// <example>01-01-1900</example>
    public DateTime DateFrom { get; set; }

    /// <summary>End date filter.</summary>
    /// <example>01-01-2000</example>
    public DateTime DateTo { get; set; }
}
