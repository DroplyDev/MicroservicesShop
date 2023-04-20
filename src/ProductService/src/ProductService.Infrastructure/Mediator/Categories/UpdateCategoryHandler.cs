using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Categories;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;

namespace ProductService.Infrastructure.Mediator.Categories;

public sealed record UpdateCategoryRequest(int Id, CategoryUpdateDto Dto) : IActionRequest;

public sealed record UpdateCategoryHandler : IActionRequestHandler<UpdateCategoryRequest>
{
    private readonly ICategoryRepo _categoryRepo;
    private readonly IValidator<CategoryUpdateDto> _validator;

    public UpdateCategoryHandler(ICategoryRepo categoryRepo, IValidator<CategoryUpdateDto> validator)
    {
        _categoryRepo = categoryRepo;
        _validator = validator;
    }

    public async ValueTask<IActionResult> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.Dto, cancellationToken);

        var product = await _categoryRepo.FirstOrDefaultAsTrackingAsync(u => u.Id == request.Id) ??
                      throw new EntityNotFoundByIdException<Category>(request.Id);
        request.Dto.Adapt(product);
        await _categoryRepo.SaveChangesAsync();
        return new NoContentResult();
    }
}
