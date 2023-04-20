using FluentValidation;

namespace ProductService.Contracts.SubTypes;

/// <summary>
///     FilterByDateOptionsValidator
/// </summary>
public sealed class FilterByDateOptionsValidator : AbstractValidator<FilterByDateOptions>
{
    /// <summary>Initializes a new instance of the <see cref="FilterByDateOptionsValidator" /> class.</summary>
    public FilterByDateOptionsValidator()
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
