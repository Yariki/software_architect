using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace CatalogService.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }

    [GraphQLIgnore]
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    [GraphQLIgnore]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    [GraphQLIgnore]
    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    [GraphQLIgnore]
    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    [GraphQLIgnore]
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
