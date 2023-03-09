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