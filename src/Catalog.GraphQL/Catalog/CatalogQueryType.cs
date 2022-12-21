using Catalog.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.GraphQL.Catalog;

[ExtendObjectType("Query")]
public class CatalogQueryType
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    public async Task<IQueryable<CatalogService.Domain.Entities.Catalog>> GetCatalogs([Service] IApplicationDbContext context) =>
        context.Catalogs;

    public async Task<CatalogService.Domain.Entities.Catalog?> GetCatalog(int id,
        [Service] IApplicationDbContext context) 
    {
        var catalog = await context.Catalogs.SingleOrDefaultAsync(c => c.Id == id);
        return catalog;
    }
}