// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Presentation;
internal static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 5000,
        Level = LogLevel.Information,
        Message = "Started {Application} in {Environment} mode with runtime {Runtime} and OS {OperatingSystem}.")]
    public static partial void ApplicationStarted(
        this ILogger logger,
        string application,
        string environment,
        string runtime,
        string operatingSystem);

    [LoggerMessage(
        EventId = 5001,
        Level = LogLevel.Information,
        Message = "Stopped {Application} in {Environment} mode with runtime {Runtime} and OS {OperatingSystem}.")]
    public static partial void ApplicationStopped(
        this ILogger logger,
        string application,
        string environment,
        string runtime,
        string operatingSystem);

    [LoggerMessage(
        EventId = 5002,
        Level = LogLevel.Critical,
        Message = "{Application} terminated unexpectedly in {Environment} mode with runtime {Runtime} and OS {OperatingSystem}.")]
    public static partial void ApplicationTerminatedUnexpectedly(
        this ILogger logger,
        Exception exception,
        string application,
        string environment,
        string runtime,
        string operatingSystem);
}
