// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Responses;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;
using ProductService.Infrastructure.Requests.Products;

namespace ProductService.Infrastructure.Handlers.Products;

public sealed record UpdateProductHandler : IActionRequestHandler<UpdateProductRequest>
{
    private readonly IProductRepo _productRepo;
    private readonly IValidator<ProductUpdateDto> _validator;

    public UpdateProductHandler(IProductRepo productRepo, IValidator<ProductUpdateDto> validator)
    {
        _productRepo = productRepo;
        _validator = validator;
    }

    public async ValueTask<IActionResult> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(BadRequestResponse.With(validationResult));
        }

        var product = await _productRepo.FirstOrDefaultAsTrackingAsync(u => u.Id == request.Id) ??
                      throw new EntityNotFoundByIdException<Product>(request.Id);
        request.Dto.Adapt(product);
        await _productRepo.SaveChangesAsync();
        return new NoContentResult();
    }
}
