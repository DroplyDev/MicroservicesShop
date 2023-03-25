// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Serilog.Events;

namespace ProductService.Domain.Exceptions;

public class ApiException : Exception
{
    private readonly LogEventLevel _logLevel = LogEventLevel.Fatal;


    public ApiException(string description, int statusCode, LogEventLevel logLevel)
    {
        Description = description;
        StatusCode = statusCode;
        _logLevel = logLevel;
    }


    public ApiException()
    {
    }


    public int StatusCode { get; } = 500;


    public string Description { get; } = "Unhandled exception occured";

    public LogEventLevel GetLevel()
    {
        return _logLevel;
    }
}
