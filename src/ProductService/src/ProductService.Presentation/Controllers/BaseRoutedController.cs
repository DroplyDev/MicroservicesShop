#region

using Microsoft.AspNetCore.Mvc;

#endregion

namespace ProductService.Presentation.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseRoutedController : BaseApiController
{
}