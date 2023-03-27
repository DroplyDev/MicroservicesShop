// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     PageOptionsValidator
/// </summary>
public sealed class PageOptionsValidator : AbstractValidator<PageOptions>
{
    /// <summary>Initializes a new instance of the <see cref="PageOptionsValidator" /> class.</summary>
    public PageOptionsValidator()
    {
        RuleFor(d => d.Offset).GreaterThanOrEqualTo(0);
        RuleFor(d => d.Limit).GreaterThanOrEqualTo(0);
    }
}
