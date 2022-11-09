namespace EventBus;

public class Message 
{
    public Guid? CorrelationId { get; set; }

    public string Payload { get; set; }
    
    public string MessageName { get; set; }
    
    public Guid MessageId { get; set; }

}