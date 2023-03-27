// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentValidation;
using ProductService.Contracts.SubTypes;
using Rusty.Template.Contracts.Requests;

#endregion

namespace ProductService.Contracts.Requests.Pagination;

/// <summary>
///     OrderByPagedRequestValidator
/// </summary>
public sealed class OrderByPagedRequestValidator : AbstractValidator<OrderedPagedRequest>
{
    /// <summary>Initializes a new instance of the <see cref="OrderByPagedRequestValidator" /> class.</summary>
    public OrderByPagedRequestValidator()
    {
        RuleFor(w => w.PageData)
            .SetValidator(new PageOptionsValidator()!)
            .When(item => item.PageData is not null);
        RuleFor(w => w.OrderByData)
            .SetValidator(new OrderByOptionsValidator());
    }
}
