using System.Runtime.Serialization;

namespace CatalogService.Domain.Exceptions;
public class CatalogException : Exception
{
    public CatalogException()
    {
    }

    public CatalogException(string? message) : base(message)
    {
    }

    public CatalogException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CatalogException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
