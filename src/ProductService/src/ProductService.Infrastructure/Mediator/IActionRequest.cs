using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Infrastructure.Mediator;

public interface IActionRequest : IRequest<IActionResult>
{
}
