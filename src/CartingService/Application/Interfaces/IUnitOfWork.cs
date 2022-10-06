using CartingService.Domain.Entities;

namespace CartingService.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICartRepository CartRepository { get; }
}