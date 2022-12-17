using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using CartingService.Application.Interfaces;
using EventBus;
using Logging;
using Serilog.Context;

namespace CartingService.Application.MessageHandlers;

public class UpdatedProductMessageHandler : IMessageHandler
{
    private readonly ICartRepository _cartRepository;
    private readonly IInboxRepository _inboxRepository;
    private readonly ILogger<UpdatedProductMessageHandler> _logger;

    public UpdatedProductMessageHandler(ICartRepository cartRepository, 
        IInboxRepository inboxRepository, 
        ILogger<UpdatedProductMessageHandler> logger)
    {
        _cartRepository = cartRepository;
        _inboxRepository = inboxRepository;
        _logger = logger;
    }

    public string MessageName => EventNames.ProductUpdatedEvent;

    public Task<bool> HandleAsync(Message message)
    {
        using var property = LogContext.PushProperty(LoggingConsts.CorrelationIdProperty, message.CorrelationId);

        var result = false;
        try
        {
            if (_inboxRepository.IsMessagePresent(message.MessageId))
            {
                _logger.LogInformation($"The message with id {message.MessageId} (CorrelationId {message.CorrelationId}) was already handled");

                result = true;
                return Task.FromResult(result);
            }

            var productDto = JsonSerializer.Deserialize<ProductDto>(message.Payload);

            var carts = _cartRepository.GetCartWithProduct(productDto.Id);

            foreach (var cart in carts)
            {
                var item = cart.Items.First(i => i.Id == productDto.Id);
                if(item == null)
                {
                    continue;
                }

                item.Price = productDto.Price;
                item.Name = productDto.Name;

                _cartRepository.UpdateCart(cart);
            }

            result = true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Problem during handling message.");
        }

        return Task.FromResult(result);
    }
}
