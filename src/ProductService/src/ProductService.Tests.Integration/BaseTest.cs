// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Tests.Integration.Factories;

namespace ProductService.Tests.Integration;

public abstract class BaseTest
{
    protected readonly WebApiFactory ApiFactory;
    protected readonly NSwagClient Client;
    protected readonly IMapper Mapper;

    protected BaseTest()
    {
        ApiFactory = new InMemoryApiFactory();
        //ApiFactory = new DockerApiFactory();

        Client = new NSwagClient(ApiFactory.CreateClient());
        var scope = ApiFactory.Services.CreateScope();
        Mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    }
}
