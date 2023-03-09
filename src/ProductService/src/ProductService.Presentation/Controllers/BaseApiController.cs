using Microsoft.AspNetCore.Mvc;
using ProductService.Contracts.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(ApiExceptionResponse))]
public abstract class BaseApiController : ControllerBase
{
}