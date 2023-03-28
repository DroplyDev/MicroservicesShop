// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Serilog.Events;

#endregion


namespace ProductService.Domain.Exceptions;

public class InsufficientPrivilegeException : ApiException
{
    protected InsufficientPrivilegeException(string message) : base(message, 403, LogEventLevel.Warning)
    {
    }


    public InsufficientPrivilegeException() : base("Permission denied", 403, LogEventLevel.Warning)
    {
    }
}
