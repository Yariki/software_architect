using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Abstractions;
using Catalog.GraphQL.Models;
using CatalogService.Infrastructure.Persistence;

namespace Catalog.GraphQL.Catalog;

[ExtendObjectType("Mutation")]
public class CatalogMutationType
{
    public async Task<CatalogDto> AddCatalog(
        CatalogInput catalogInput, 
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var catalog = new CatalogService.Domain.Entities.Catalog
        {
            Name = catalogInput.Name,
            Image = catalogInput.Image,
            CatalogId = catalogInput.CatalogId
        };

        await context.Catalogs.AddAsync(catalog, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return CatalogDto.FromCatalog(catalog);
    }

    public async Task<CatalogDto> UpdateCatalog(
        CatalogDto catalogDto,
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var catalog = new CatalogService.Domain.Entities.Catalog
        {
            Id = catalogDto.Id,
            Name = catalogDto.Name,
            Image = catalogDto.Image,
            CatalogId = catalogDto.CatalogId
        };

        context.Catalogs.Update(catalog);
        await context.SaveChangesAsync(cancellationToken);

        return CatalogDto.FromCatalog(catalog);
    }

    public async Task<bool> DeleteCatalog(int id,
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var catalog = context.Catalogs.Find(id);
        if(catalog == null)
        {
            return false;
        }
        context.Catalogs.Remove(catalog);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
