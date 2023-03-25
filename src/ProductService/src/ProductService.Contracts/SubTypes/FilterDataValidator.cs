// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     FilterDataValidator
/// </summary>
public sealed class FilterDataValidator : AbstractValidator<FilterData>
{
    /// <summary>Initializes a new instance of the <see cref="FilterDataValidator" /> class.</summary>
    public FilterDataValidator()
    {
        RuleFor(d => d.DateFrom)
            .LessThanOrEqualTo(DateTime.Now)
            .GreaterThanOrEqualTo(DateTime.MinValue);
        RuleFor(d => d.DateTo)
            .GreaterThanOrEqualTo(DateTime.MinValue)
            .LessThanOrEqualTo(DateTime.MaxValue)
            .GreaterThanOrEqualTo(d => d.DateTo);
    }
}
