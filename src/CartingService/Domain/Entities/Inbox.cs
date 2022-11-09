using LiteDB;

namespace CartingService.Domain.Entities;

public class Inbox
{
    [BsonId]
    public Guid Id { get; set; }

    public Guid MessageId { get; set; }

    public string Payload { set; get; }
}
