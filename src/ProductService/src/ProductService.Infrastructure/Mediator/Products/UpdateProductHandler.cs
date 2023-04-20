using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Repositories;
using ProductService.Contracts.Dtos.Products;
using ProductService.Domain;
using ProductService.Domain.Exceptions.Entity;

namespace ProductService.Infrastructure.Mediator.Products;

public sealed record UpdateProductRequest(int Id, ProductUpdateDto Dto) : IActionRequest;

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
		await _validator.ValidateAndThrowAsync(request.Dto, cancellationToken);

		var product = await _productRepo.FirstOrDefaultAsTrackingAsync(u => u.Id == request.Id) ??
					  throw new EntityNotFoundByIdException<Product>(request.Id);
		request.Dto.Adapt(product);
		await _productRepo.SaveChangesAsync();
		return new NoContentResult();
	}
}
