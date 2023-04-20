region

using FluentValidation;
using ProductService.Contracts.SubTypes;

#endregion

namespace ProductService.Contracts.Requests.Pagination;

/// <summary>
///     FilteredOrderedPagedRequestValidator
/// </summary>
public sealed class FilteredOrderedPagedRequestValidator : AbstractValidator<FilterOrderPageRequest>
{
    /// <summary>Initializes a new instance of the <see cref="FilteredOrderedPagedRequestValidator" /> class.</summary>
    public FilteredOrderedPagedRequestValidator()
    {
        RuleFor(w => w.FilterData)
            .SetValidator(new FilterByDateOptionsValidator()!)
            .When(item => item.FilterData is not null);
        RuleFor(w => w.PageData)
            .SetValidator(new PageOptionsValidator()!)
            .When(item => item.PageData is not null);
        RuleFor(w => w.OrderByData)
            .SetValidator(new OrderByOptionsValidator());
    }
}
