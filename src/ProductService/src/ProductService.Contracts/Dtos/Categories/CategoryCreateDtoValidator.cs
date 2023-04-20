using FluentValidation;

namespace ProductService.Contracts.Dtos.Categories;

public sealed class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(p => p.Description)
            .MaximumLength(500);
    }
}
