using Serilog.Events;

namespace ProductService.Domain.Exceptions;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
public class InsufficientPrivilegeException : ApiException
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
{
    protected InsufficientPrivilegeException(string message) : base(message, 403, LogEventLevel.Warning)
    {
    }


    public InsufficientPrivilegeException() : base("Permission denied", 403, LogEventLevel.Warning)
    {
    }
}
