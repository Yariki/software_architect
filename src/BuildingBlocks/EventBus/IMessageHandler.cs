namespace EventBus;

public interface IMessageHandler
{
    string MessageName { get;}
    
    Task<bool> HandleAsync(Message message);
}