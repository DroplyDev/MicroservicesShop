// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     PageDataValidator
/// </summary>
public sealed class PageDataValidator : AbstractValidator<PageData>
{
    /// <summary>Initializes a new instance of the <see cref="PageDataValidator" /> class.</summary>
    public PageDataValidator()
    {
        RuleFor(d => d.Offset).GreaterThanOrEqualTo(0);
        RuleFor(d => d.Limit).GreaterThanOrEqualTo(0);
    }
}
