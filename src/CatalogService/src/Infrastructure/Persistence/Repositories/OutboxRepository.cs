using Catalog.Abstractions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CatalogService.Infrastructure.Persistence.Repositories;

public class OutboxRepository : IOutboxRepository
{
    private IApplicationDbContext _applicationDbContext;
    
    public OutboxRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public IDbContextTransaction BeginTransaction()
    {
        return _applicationDbContext.BeginTransaction();
    }
    
    public Task<List<Outbox>> GetNewOutboxMessagesAsync(int take = 2)
    {
        return _applicationDbContext
            .Outboxes
            .Where(o => o.Status == OutBoxStatus.New)
            .Take(take)
            .ToListAsync();
    }
    
    public Task UpdateAsync(Outbox outbox)
    {
        ((ApplicationDbContext)_applicationDbContext).Update(outbox);
        return _applicationDbContext.SaveChangesAsync(CancellationToken.None);
    }
    
    
}