using System.Reflection;
using Catalog.Abstractions;
using CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CatalogService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Domain.Entities.Catalog> Catalogs => Set<Domain.Entities.Catalog>();

    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Outbox> Outboxes => Set<Outbox>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    public IDbContextTransaction BeginTransaction()
    {
        return Database.BeginTransaction();
    }

}
