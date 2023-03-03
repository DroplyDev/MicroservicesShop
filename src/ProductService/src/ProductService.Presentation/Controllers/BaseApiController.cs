﻿#region

using Microsoft.AspNetCore.Mvc;
using ProductService.Contracts.Responses;
using ProductService.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace ProductService.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(ApiExceptionResponse))]
public abstract class BaseApiController : ControllerBase
{
}