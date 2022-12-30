using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Abstractions;

public interface IApplicationDbContext
{
    DbSet<CatalogService.Domain.Entities.Catalog> Catalogs { get; }

    DbSet<CatalogService.Domain.Entities.Product> Products { get; }
    DbSet<Outbox> Outboxes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    IDbContextTransaction BeginTransaction();
}
