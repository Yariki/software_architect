using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CatalogService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Catalog> Catalogs { get; }

    DbSet<Domain.Entities.Product> Products { get; }
    DbSet<Outbox> Outboxes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    IDbContextTransaction BeginTransaction();
}
