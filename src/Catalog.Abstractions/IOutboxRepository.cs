using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace CatalogService.Application.Common.Interfaces;

public interface IOutboxRepository
{
    IDbContextTransaction BeginTransaction();
    Task<List<Outbox>> GetNewOutboxMessagesAsync(int take = 2);
    Task UpdateAsync(Outbox outbox);
}