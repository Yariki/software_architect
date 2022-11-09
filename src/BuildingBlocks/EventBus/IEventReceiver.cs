namespace EventBus;

public interface IEventReceiver : IAsyncDisposable
{
    Task StartReceiveMessageAsync();
}