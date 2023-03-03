using FluentValidation;

namespace ProductService.Contracts.Dtos.Products;

public sealed class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
	public ProductUpdateDtoValidator()
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