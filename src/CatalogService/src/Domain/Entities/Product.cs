namespace CatalogService.Domain.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int Image { get; set; }

    public int CatalogId { get; set; }

    public Catalog Catalog { get; set; }

    public decimal Price { get; set; }

    public uint Amount { get; set; }

    public void AddAmount(uint amount)
    {
        Amount += amount;
    }

    public void RemoveAmount(uint amount)
    {
        if (Amount == 0)
        {
            throw new CatalogException($"There is no {Name} in stock");
        }

        if (Amount < amount)
        {
            throw new CatalogException($"Not enough amount of product {Name} in stock");
        }

        Amount -= amount;
    }

    public bool IsEnoughAmount(uint amount)
    {
        return Amount >= amount;
    }

}