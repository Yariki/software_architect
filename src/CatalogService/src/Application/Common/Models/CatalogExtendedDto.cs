using CatalogService.Application.Common.Mappings;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Models;

public class CatalogExtendedDto : BaseDto , IMapFrom<Catalog>
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Image { get; set; }

    public int? CatalogId { get; set; }
    
    public CatalogDto Parent{ get; set; }
    
    public IEnumerable<CatalogDto> Childrens { get; set; }
}