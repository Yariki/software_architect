using System.Runtime.Serialization;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace EventBus;

public class EventSender : IEventSender
{
    private readonly AzureServiceBusProducerConfiguration _configuration;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusSender _serviceBusSender;
    
    public EventSender(IOptions<AzureServiceBusProducerConfiguration> configuration)
    {
        _configuration = configuration.Value;
        _serviceBusClient = new ServiceBusClient(_configuration.ConnectionString);
        _serviceBusSender = _serviceBusClient.CreateSender(_configuration.Queue);
    }
    
    public async Task<SendResult> SendEventAsync(Message @event)
    {
        var result = SendResult.None;
        try
        {
            var json = JsonSerializer.Serialize(@event);
            var message = new ServiceBusMessage()
            {
                MessageId = @event.MessageId.ToString(),
                Body = new BinaryData(json),
                Subject = @event.MessageName
            };
        
            await _serviceBusSender.SendMessageAsync(message);

            result = SendResult.Acknowledged;
        }
        catch (Exception e) when (e is ServiceBusException || e is SerializationException)
        {
            result = SendResult.RecoverableFailure;
        }
        catch(NotSupportedException e)
        {
            result = SendResult.NoneRecoverableFailure;
        }
        catch (Exception e)
        {
            result = SendResult.NoneRecoverableFailure;
        }
        
        return result;
    }

    public async ValueTask DisposeAsync()
    {
        await _serviceBusSender.DisposeAsync();
        await _serviceBusClient.DisposeAsync();
    }
}