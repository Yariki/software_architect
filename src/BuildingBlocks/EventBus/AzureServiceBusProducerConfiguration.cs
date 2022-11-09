namespace EventBus;

public class AzureServiceBusProducerConfiguration
{
    public string ConnectionString { get; set; }
    
    public string Queue { get; set; }
}