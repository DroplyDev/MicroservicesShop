#region

using Microsoft.AspNetCore.Mvc;
using ProductService.Contracts.Responses;
using ProductService.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace ProductService.Presentation.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]

public abstract class BaseRoutedController : BaseApiController
{
}