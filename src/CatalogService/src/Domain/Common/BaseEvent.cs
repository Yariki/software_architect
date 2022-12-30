using MediatR;

namespace CatalogService.Domain.Common;

public abstract class BaseEvent : INotification
{
    protected BaseEvent()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}
