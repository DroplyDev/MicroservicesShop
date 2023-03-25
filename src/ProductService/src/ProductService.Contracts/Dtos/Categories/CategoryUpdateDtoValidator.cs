using FluentValidation;

namespace ProductService.Contracts.Dtos.Categories;

public sealed class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
	public CategoryUpdateDtoValidator()
	{
		RuleFor(p => p.Name)
			.NotEmpty()
			.MaximumLength(50);
		RuleFor(p => p.Description)
			.MaximumLength(500);
	}
}
