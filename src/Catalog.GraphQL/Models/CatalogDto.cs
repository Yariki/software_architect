using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.GraphQL.Models;
public class CatalogDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public int CatalogId { get; set; }

    public static CatalogDto FromCatalog(CatalogService.Domain.Entities.Catalog catalog)
    {
        
        return catalog != null ? new CatalogDto()
        {
            Id = catalog.Id,
            Name = catalog.Name,
            Image = catalog.Image,
            CatalogId = catalog.CatalogId.Value
        }
        : Empty();
    }

    public static CatalogDto Empty() => new();
}
