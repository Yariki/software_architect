using Catalog.Abstractions;
using Catalog.GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.GraphQL.Catalog;

[ExtendObjectType("Query")]
public class CatalogQueryType
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    public Task<List<CatalogDto>> GetCatalogs([Service] IApplicationDbContext context) =>
        context.Catalogs.Select(c => CatalogDto.FromCatalog(c)).ToListAsync();

    public async Task<CatalogDto> GetCatalog(int id,
        [Service] IApplicationDbContext context) 
    {
        var catalog = await context.Catalogs.SingleOrDefaultAsync(c => c.Id == id);
        return CatalogDto.FromCatalog(catalog);
    }
}