using AutoMapper;

using CatalogService.Application.Common.Mappings;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Models;

public class CatalogExtendedDto : BaseDto , IMapFrom<Domain.Entities.Catalog>
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Image { get; set; }

    public int? CatalogId { get; set; }
    
    public CatalogDto Parent{ get; set; }
    
    public IEnumerable<CatalogDto> Childrens { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(Domain.Entities.Catalog), GetType())
            .ForMember("Links", o => o.Ignore() );
    }
}