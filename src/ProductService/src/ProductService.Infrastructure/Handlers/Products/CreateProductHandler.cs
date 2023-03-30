// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record CreateProductHandler : IActionRequestHandler<CreateProductRequest>
{
    private readonly IProductRepo _productRepo;
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly IValidator<ProductCreateDto> _validator;

    public CreateProductHandler(IProductRepo productRepo, IActionContextAccessor actionContextAccessor, IValidator<ProductCreateDto> validator)
    {
        _productRepo = productRepo;
        _actionContextAccessor = actionContextAccessor;
        _validator = validator;
    }

    public async ValueTask<IActionResult> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.Dto, cancellationToken);
        if (await _productRepo.ExistsAsync(p => p.Name == request.Dto.Name, cancellationToken))
        {
            throw new EntityWitNameAlreadyExistsException<Product>(request.Dto.Name);
        }
        var product = await _productRepo.AddAsync(request.Dto.Adapt<Product>());
        return new CreatedAtActionResult("GetProductById", "Products", new { id = product.Id }, product.Adapt<ProductDto>());
    }
}
