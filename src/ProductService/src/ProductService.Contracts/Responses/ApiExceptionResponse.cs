// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProductService.Contracts.Responses;

/// <summary>
///     Api exception response model
/// </summary>
public sealed record ApiExceptionResponse
{
    /// <summary>Initializes a new instance of the <see cref="ApiExceptionResponse" /> class.</summary>
    /// <param name="title">The title.</param>
    /// <param name="statusCode">The status code.</param>
    public ApiExceptionResponse(string title, int statusCode)
    {
        Title = title;
        StatusCode = statusCode;
    }

    /// <summary>Initializes a new instance of the <see cref="ApiExceptionResponse" /> class.</summary>
    public ApiExceptionResponse()
    {
    }

    /// <summary>Exception title.</summary>
    /// <example>Exception</example>
    public string Title { get; init; } = null!;

    /// <summary>Exception status code.</summary>
    /// <value>200</value>
    public int StatusCode { get; init; }
}
