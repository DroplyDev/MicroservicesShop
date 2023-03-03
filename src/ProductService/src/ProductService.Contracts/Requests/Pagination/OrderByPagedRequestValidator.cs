﻿#region

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
			.SetValidator(new PageDataValidator()!)
			.When(item => item.PageData is not null);
		RuleFor(w => w.OrderByData)
			.SetValidator(new OrderByDataValidator());
	}
}