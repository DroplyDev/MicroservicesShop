// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit.Abstractions;

namespace ProductService.Tests.Integration.TestOrder;

public class ClassOrderer
{
    private const string Prefix = "ProductService.Tests.Integration";

    private readonly List<string> _classOrder = new()
    {
        //$"{Prefix}.ProductPaginationTest",
    };

    public IEnumerable<ITestCollection> OrderTestCollections(List<ITestCollection> testCollections)
    {
        var sortedCollections = new List<ITestCollection>();
        foreach (var className in _classOrder)
        {
            var collection = testCollections.SingleOrDefault(x => x.DisplayName.Contains(className));
            if (collection != null)
            {
                sortedCollections.Add(collection);
            }
        }

        return sortedCollections;
    }
}
