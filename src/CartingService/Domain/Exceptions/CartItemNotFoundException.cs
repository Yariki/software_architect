namespace CartingService.Domain.Exceptions;

public class CartItemNotFoundException : CartServiceException
{
    public CartItemNotFoundException()
    {
    }

    public CartItemNotFoundException(string? message) : base(message)
    {
    }

    public CartItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}