// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Infrastructure.Handlers;

public interface IActionRequestHandler<in TRequest> : IRequestHandler<TRequest, IActionResult>
    where TRequest : IRequest<IActionResult>
{
}
