using System.Text.Json;
using Catalog.Abstractions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Events;
using EventBus;
using Logging.Services;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Product.EventHandlers;

public class UpdateProductEventHandler : INotificationHandler<UpdatedProductEvent>
{

    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public UpdateProductEventHandler(IApplicationDbContext applicationDbContext, ICorrelationIdGenerator correlationIdGenerator)
    {
        _applicationDbContext = applicationDbContext;
        _correlationIdGenerator = correlationIdGenerator;
    }
    
    public Task Handle(UpdatedProductEvent notification, CancellationToken cancellationToken)
    {
        var outbox = new Outbox()
        {
            Payload = JsonSerializer.Serialize(notification.Product),
            EventName = EventNames.ProductUpdatedEvent,
            Status = OutBoxStatus.New,
            CorrelationId = Guid.Parse(_correlationIdGenerator.CorrelationId),
            EventId = Guid.NewGuid(),
            CreateDate = DateTime.UtcNow
        };
        _applicationDbContext.Outboxes.Add(outbox);
        return _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}