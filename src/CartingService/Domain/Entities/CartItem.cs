namespace CartingService.Domain.Entities;

public class CartItem
{
    //todo is this looks ok to you? e.g. when we are using this constructor does the model in the valid state?
    public CartItem()
    {
        
    }
    
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}