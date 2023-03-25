// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;

namespace ProductService.Contracts.Dtos.Products;

public sealed class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
	public ProductCreateDtoValidator()
	{
		RuleFor(p => p.Name)
			.NotEmpty()
			.MaximumLength(50);
		RuleFor(p => p.Description)
			.MaximumLength(500);
		RuleFor(p => p.Price)
			.GreaterThan(0);
		RuleFor(p => p.Quantity)
			.GreaterThanOrEqualTo(0);
	}
}
