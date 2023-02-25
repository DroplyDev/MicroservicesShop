#region

#endregion

using Serilog.Events;

namespace ProductService.Domain.Exceptions.Entity;

public abstract class BaseEntityException : ApiException
{
    protected BaseEntityException(string message, int statusCode, LogEventLevel logEventLevel) : base(message,
        statusCode, logEventLevel)
    {
    }
}