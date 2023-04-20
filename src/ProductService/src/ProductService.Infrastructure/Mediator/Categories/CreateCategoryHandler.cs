using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Categories;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;

namespace ProductService.Infrastructure.Mediator.Categories;

public sealed record CreateCategoryRequest(CategoryCreateDto Dto) : IActionRequest;

public sealed record CreateCategoryHandler : IActionRequestHandler<CreateCategoryRequest>
{
    private readonly ICategoryRepo _categoryRepo;
    private readonly IValidator<CategoryCreateDto> _validator;

    public CreateCategoryHandler(ICategoryRepo categoryRepo, IValidator<CategoryCreateDto> validator)
    {
        _categoryRepo = categoryRepo;
        _validator = validator;
    }

    public async ValueTask<IActionResult> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.Dto, cancellationToken);
        if (await _categoryRepo.ExistsAsync(p => p.Name == request.Dto.Name, cancellationToken))
        {
            throw new EntityWitNameAlreadyExistsException<Category>(request.Dto.Name);
        }

        var category = await _categoryRepo.AddAsync(request.Dto.Adapt<Category>());
        return new CreatedAtActionResult("GetCategoryById", "Categories", new {id = category.Id},
            category.Adapt<CategoryDto>());
    }
}
