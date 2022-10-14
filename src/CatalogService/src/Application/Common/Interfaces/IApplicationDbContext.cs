using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Catalog> Catalogs { get; }

    DbSet<Domain.Entities.Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
