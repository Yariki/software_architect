using CartingService.Domain.Entities;

namespace CartingService.Application.Interfaces;

public interface IInboxRepository
{
    void AddInbox(Inbox inbox);
    bool IsMessagePresent(Guid messageId);
}
