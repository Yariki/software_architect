using CartingService.Application.Interfaces;
using CartingService.Domain.Entities;
using LiteDB;

namespace CartingService.Infrastructure.Persistance;

public class InboxRepository : IInboxRepository
{
    private LiteCollection<Inbox> _inboxes;

    public InboxRepository(IApplicationDbContext applicationDbContext)
    {
        _inboxes = applicationDbContext.Database.GetCollection<Inbox>() as LiteCollection<Inbox>;
    }
    
    public bool IsMessagePresent(Guid messageId)
    {
        return _inboxes.FindOne(i => i.MessageId == messageId) != null;
    }

    public void AddInbox(Inbox inbox)
    {
        _inboxes.Insert(inbox);
    }

}
