using CartingService.Domain.Exceptions;
using LiteDB;

namespace CartingService.Domain.Entities;

public class Cart
{
    public Cart()
    {
        
    }
    
    [BsonId]
    public Guid Id { get; set; }

    public IList<CartItem> Items { get; private set; } = new List<CartItem>();
}