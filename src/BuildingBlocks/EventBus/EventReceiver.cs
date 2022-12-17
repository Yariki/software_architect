using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventBus;

public class EventReceiver : IEventReceiver
{
    private readonly AzureServiceBusProducerConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<EventReceiver> _logger;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusProcessor _serviceBusProcessor;
    
    public EventReceiver(IOptions<AzureServiceBusProducerConfiguration> configuration, 
        IServiceScopeFactory scopeFactory, ILogger<EventReceiver> logger)
    {
        _configuration = configuration.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
        _serviceBusClient = new ServiceBusClient(_configuration.ConnectionString);
        _serviceBusProcessor = _serviceBusClient.CreateProcessor(_configuration.Queue);
    }

    public Task StartReceiveMessageAsync()
    {
        _serviceBusProcessor.ProcessMessageAsync += ServiceBusProcessorOnProcessMessageAsync;
        _serviceBusProcessor.ProcessErrorAsync += ServiceBusProcessorOnProcessErrorAsync;
        return _serviceBusProcessor.StartProcessingAsync();
    }


    private async Task ServiceBusProcessorOnProcessMessageAsync(ProcessMessageEventArgs arg)
    {
        try
        {
            var result = await ProcessEventAsync(arg.Message);
            if (!result)
            {
                _logger.LogWarning($"The message {arg.Message.MessageId} was not processed");
                return;
            }

            await arg.CompleteMessageAsync(arg.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while processing message");
        }
    }
    
    private Task ServiceBusProcessorOnProcessErrorAsync(ProcessErrorEventArgs arg)
    {
        var ex = arg.Exception;
        var context = arg.ErrorSource;

        _logger.LogError(ex, "ERROR handling message: {ExceptionMessage} - Context: {@ExceptionContext}", ex.Message, context);

        return Task.CompletedTask;
    }

    private Task<bool> ProcessEventAsync(ServiceBusReceivedMessage sbMessage)
    {
        using var scope = _scopeFactory.CreateScope();
        var handlers = scope.ServiceProvider.GetRequiredService<IEnumerable<IMessageHandler>>();

        var handler = handlers.FirstOrDefault(h => h.MessageName == sbMessage.Subject);
        if (handler == null)
        {
            _logger.LogWarning($"The handler for message - {sbMessage.Subject} - is not registered");
            return Task.FromResult(false);
        }

        var message = JsonSerializer.Deserialize<Message>(sbMessage.Body.ToString()); 
        
        return handler.HandleAsync(message);
    }
 
    public async ValueTask DisposeAsync()
    {
        await _serviceBusProcessor.DisposeAsync();
        await _serviceBusClient.DisposeAsync();
    }
}