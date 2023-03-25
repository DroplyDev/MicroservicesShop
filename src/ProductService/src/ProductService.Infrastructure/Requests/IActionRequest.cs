using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Infrastructure.Requests;

public interface IActionRequest : IRequest<IActionResult>
{
}
