using HotChocolate;

namespace CatalogService.Domain.Entities;
public class Product : BaseEntity
{
    private uint _amount;
    
    private Product(){}

    public Product(uint amount = 0)
    {
        _amount = amount;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Image { get; set; }

    public int CatalogId { get; set; }

    [GraphQLIgnore]
    public Catalog Catalog { get; set; }

    public decimal Price { get; set; }

    public uint Amount { get => _amount; }
    
    [GraphQLIgnore]
    public void AddAmount(uint amount)
    {
        _amount += amount;
    }
    
    [GraphQLIgnore]
    public void RemoveAmount(uint amount)
    {
        if (_amount == 0)
        {
            throw new CatalogException($"There is no {Name} in stock");
        }

        if (_amount < amount)
        {
            throw new CatalogException($"Not enough amount of product {Name} in stock");
        }

        _amount -= amount;
    }

    [GraphQLIgnore]
    public bool IsEnoughAmount(uint amount)
    {
        return _amount >= amount;
    }

}