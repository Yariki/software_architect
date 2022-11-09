namespace EventBus;

public interface IEventSender : IAsyncDisposable
{
    Task<SendResult> SendEventAsync(Message message);
}