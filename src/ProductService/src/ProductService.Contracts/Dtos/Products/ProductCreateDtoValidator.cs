﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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