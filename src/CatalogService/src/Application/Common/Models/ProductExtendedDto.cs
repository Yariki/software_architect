using CatalogService.Application.Common.Mappings;

namespace CatalogService.Application.Common.Models;

public class ProductExtendedDto : BaseDto, IMapFrom<Domain.Entities.Product>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Image { get; set; }

    public int CatalogId { get; set; }

    public decimal Price { get; set; }

    public uint Amount { get; set; }
}