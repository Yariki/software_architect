using HotChocolate;

namespace CatalogService.Domain.Entities;

public class Catalog : BaseEntity
{
    public string Name { get; set; }

    public string Image { get; set; }

    public int? CatalogId { get; set; }
    
    public virtual Catalog Parent { get; set; }

    public virtual ICollection<Catalog> Childrens { get; set; }
    
    public virtual ICollection<Product> Products { get; set; }

}
