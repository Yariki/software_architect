using System.Text.Json;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Events;
using EventBus;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Product.EventHandlers;

public class UpdateProductEventHandler : INotificationHandler<UpdatedProductEvent>
{

    private readonly IApplicationDbContext _applicationDbContext;
    
    public UpdateProductEventHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public Task Handle(UpdatedProductEvent notification, CancellationToken cancellationToken)
    {
        var outbox = new Outbox()
        {
            Payload = JsonSerializer.Serialize(notification.Product),
            EventName = EventNames.ProductUpdatedEvent,
            Status = OutBoxStatus.New,
            CorrelationId = Guid.NewGuid(),
            EventId = Guid.NewGuid(),
            CreateDate = DateTime.UtcNow
        };
        _applicationDbContext.Outboxes.Add(outbox);
        return _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}