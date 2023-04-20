using FluentValidation;

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     OrderByOptionsValidator
/// </summary>
public sealed class OrderByOptionsValidator : AbstractValidator<OrderByData>
{
    /// <summary>Initializes a new instance of the <see cref="OrderByOptionsValidator" /> class.</summary>
    public OrderByOptionsValidator()
    {
        RuleFor(d => d.OrderBy).NotEmpty();
        RuleFor(d => d.OrderDirection).IsInEnum();
    }
}
