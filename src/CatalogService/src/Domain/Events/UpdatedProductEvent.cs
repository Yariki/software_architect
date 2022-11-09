namespace CatalogService.Domain.Events;

public class UpdatedProductEvent : BaseEvent
{
    public UpdatedProductEvent(Product product)
    {
        Product = product;
    }

    public Product Product { get; private set; }
    
}