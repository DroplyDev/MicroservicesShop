// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     OrderByDataValidator
/// </summary>
public sealed class OrderByDataValidator : AbstractValidator<OrderByData>
{
    /// <summary>Initializes a new instance of the <see cref="OrderByDataValidator" /> class.</summary>
    public OrderByDataValidator()
    {
        RuleFor(d => d.OrderBy).NotEmpty();
        RuleFor(d => d.OrderDirection).IsInEnum();
    }
}
