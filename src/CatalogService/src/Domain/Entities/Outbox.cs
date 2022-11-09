namespace CatalogService.Domain.Entities;

public class Outbox : BaseEntity
{
    public Guid EventId { get; set; }

    public Guid? CorrelationId { get; set; }

    public string EventName { get; set; }

    public string Payload { get; set; }

    public DateTime CreateDate { get; set; }

    public OutBoxStatus Status { get; set; }

    public DateTime? PublishedDate { get; set; }
}