// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Contracts.SubTypes;

/// <summary>Page data subtype</summary>
public sealed class PageData
{
    /// <summary>Item offset.</summary>
    /// <example>0</example>
    public int Offset { get; set; }

    /// <summary>Item limit.</summary>
    /// <example>50</example>
    public int Limit { get; set; }
}
