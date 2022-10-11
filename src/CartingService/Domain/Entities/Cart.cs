using CartingService.Application.Exceptions;
using CartingService.Domain.Exceptions;
using LiteDB;
using MediatR;

namespace CartingService.Domain.Entities;

public class Cart
{
    public Cart()
    {
        
    }
    
    [BsonId]
    public Guid Id { get; set; }

    public IList<CartItem> Items { get; private set; } = new List<CartItem>();


    public void AddItem(CartItem item)
    {
		var existingItem = Items.FirstOrDefault(x => x.Id == item.Id);
		if (existingItem == null)
		{
			Items.Add(item);
		}
		else
		{
			existingItem.Quantity += item.Quantity;
			existingItem.Price = item.Price;
		}
	}

	public void RemoveItem(int id)
	{
		var item = Items.FirstOrDefault(x => x.Id == id);
		if (item == null)
		{
			throw new CartItemNotFoundException("The cart item does not exist");
		}

		Items.Remove(item);
	}
}