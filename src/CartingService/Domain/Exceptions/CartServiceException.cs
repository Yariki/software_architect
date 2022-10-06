namespace CartingService.Domain.Exceptions;

public class CartServiceException : Exception
{
    public CartServiceException()
    {
    }

    public CartServiceException(string? message) : base(message)
    {
    }

    public CartServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}