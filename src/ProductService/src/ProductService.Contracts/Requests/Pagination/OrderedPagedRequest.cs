﻿using ProductService.Contracts.SubTypes;


// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

/// <summary>
///     Request with order by and pagination
/// </summary>
public class OrderedPagedRequest
{
    /// <summary>Page data class.</summary>
    public PageOptions? PageData { get; init; }

    /// <summary>Order by data class.</summary>
    public OrderByData OrderByData { get; init; } = null!;
}
