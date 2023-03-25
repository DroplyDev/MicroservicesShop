// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit.Abstractions;
using Xunit.Sdk;

namespace ProductService.Tests.Integration.TestOrder;

public class MethodOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var orderedTestCases = testCases
            .OrderBy(testCase => testCase.TestMethod.Method.Name)
            .ToList();

        return orderedTestCases;
    }
}
