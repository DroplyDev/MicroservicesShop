using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Infrastructure.Mediator;

public interface IActionRequestHandler<in TRequest> : IRequestHandler<TRequest, IActionResult>
	where TRequest : IRequest<IActionResult>
{
}
