using CatalogService.Application.Common.Mappings;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;

namespace CatalogService.Application.Common.Models;

public class CatalogDto : IMapFrom<Domain.Entities.Catalog>
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Image { get; set; }

    public int? CatalogId { get; set; }
    
    public CatalogDto Parent{ get; set; }
    
    public IEnumerable<CatalogDto> Childrens { get; set; }

}