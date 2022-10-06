using CartingService.Domain.Exceptions;

namespace CartingService.Domain.Entities;

public class Cart
{
    public Cart()
    {
        
    }
    
    
    public Guid Id { get; set; }

    public IList<CartItem> Items { get; private set; } = new List<CartItem>();
}