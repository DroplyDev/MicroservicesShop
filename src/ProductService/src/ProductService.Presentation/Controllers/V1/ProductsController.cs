#region

using Mediator;
using Microsoft.AspNetCore.Mvc;
using ProductService.Contracts.Dtos.Products;
using ProductService.Contracts.Requests.Pagination;
using ProductService.Contracts.Responses;
using ProductService.Infrastructure.Attributes;
using ProductService.Infrastructure.Mediator.Products;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace ProductService.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public sealed class ProductsController : BaseRoutedController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Get paged Products",
        Description = "Returns paged list"
    )]
    [SwaggerResponse(StatusCodes.Status200OK,
        "Products retrieved successfully",
        typeof(PagedResponse<ProductDto>)
    )]
    [HttpPost("paged")]
    public async Task<IActionResult> GetFilteredPagedProductsAsync(FilterOrderPageRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPagedProductsRequest(request), cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get Product by id",
        Description = "Returns paged list"
    )]
    [SwaggerResponse(StatusCodes.Status200OK,
        "Product retrieved successfully",
        typeof(ProductDto)
    )]
    [SwaggerResponse(StatusCodes.Status404NotFound,
        "Product with id was not found",
        typeof(ApiExceptionResponse)
    )]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync([SwaggerParameter("The product id")] int id,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetProductByIdRequest(id), cancellationToken);
    }

    [SwaggerOperation(Summary = "Get Product to update by id",
        Description = "Returns Product dto for update"
    )]
    [SwaggerResponse(StatusCodes.Status200OK,
        "Product retrieved successfully",
        typeof(ProductUpdateDto)
    )]
    [SwaggerResponse(StatusCodes.Status404NotFound,
        "Product with id was not found",
        typeof(ApiExceptionResponse)
    )]
    [HttpGet("update/{id:int}")]
    public async Task<IActionResult> GetProductToUpdateByIdAsync([SwaggerParameter("The product id")] int id,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetProductToUpdateByIdRequest(id), cancellationToken);
    }

    [SwaggerOperation(Summary = "Create new Product",
        Description = "Creates new Product"
    )]
    [SwaggerResponse(
        StatusCodes.Status201Created, "Product created successfully",
        typeof(ProductDto)
    )]
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(ProductCreateDto dto)
    {
        return await _mediator.Send(new CreateProductRequest(dto));
    }

    [SwaggerOperation(Summary = "Update Product",
        Description = "Updates existing Product"
    )]
    [SwaggerResponse(
        StatusCodes.Status204NoContent, "Product updated successfully"
    )]
    [HttpPut("{id:int}")]
    [HttpPutIdCompare]
    public async Task<IActionResult> UpdateProductAsync([SwaggerParameter("The product id")] int id,
        ProductUpdateDto dto)
    {
        return await _mediator.Send(new UpdateProductRequest(id, dto));
    }

    [SwaggerOperation(Summary = "Delete Product",
        Description = "Deletes existing Product"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent,
        "Product deleted successfully"
    )]
    [SwaggerResponse(StatusCodes.Status404NotFound,
        "Product with id was not found",
        typeof(ApiExceptionResponse)
    )]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProductAsync([SwaggerParameter("The product id")] int id)
    {
        return await _mediator.Send(new DeleteProductRequest(id));
    }
}
