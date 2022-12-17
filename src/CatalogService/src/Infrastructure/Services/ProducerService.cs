using EventBus;
using  System.Threading;
using  System.Threading.Tasks;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Services;

public class ProducerService : IHostedService
{
    private Timer _timer = null;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ProducerService> _logger;

    private readonly object _lock = new();
    private int _count = 0;
    
    public ProducerService(IServiceScopeFactory scopeFactory, ILogger<ProducerService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        DoWorkAsync().Wait();
    }

    private async Task DoWorkAsync()
    {
        lock (_lock)
        {
            if (_count > 0)
            {
                _logger.LogInformation("ProducerService is already processing a message");
                return;
            }
            _count += 1;
        }
        
        using var scope = _scopeFactory.CreateScope();

        var outboxRepo = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
        var eventSender = scope.ServiceProvider.GetRequiredService<IEventSender>();
        var transaction = outboxRepo.BeginTransaction();

        try
        {
            var messages = await outboxRepo.GetNewOutboxMessagesAsync();
            foreach (var message in messages)
            {
                await UpdateOutboxStatusAsync(message, OutBoxStatus.Pushing, outboxRepo);
                var result = await PublishAsync(message, eventSender);
                await UpdateOutboxAsync(message, result, outboxRepo);
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing messages");
            await transaction.RollbackAsync();
        }
        finally
        {
            lock (_lock)
            {
                _count--;
            }
        }
    }

    private Task UpdateOutboxAsync(Outbox outbox, SendResult result, IOutboxRepository outboxRepo)
    {
        var newStatus =  result switch
        {
            SendResult.Acknowledged => OutBoxStatus.Pushed,
            SendResult.RecoverableFailure => OutBoxStatus.New,
            SendResult.NoneRecoverableFailure => OutBoxStatus.Failed,
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, message: null)
        };
        outbox.PublishedDate = DateTime.UtcNow;
        return UpdateOutboxStatusAsync(outbox, newStatus, outboxRepo);
    }
    
    private Task UpdateOutboxStatusAsync(Outbox outbox, OutBoxStatus status, IOutboxRepository repo)
    {
        outbox.Status = status;
        return repo.UpdateAsync(outbox);
    }

    private Task<SendResult> PublishAsync(Outbox outbox, IEventSender sender)
    {
        var message = new Message()
        {
            CorrelationId = outbox.CorrelationId, 
            Payload = outbox.Payload, 
            MessageId = outbox.EventId, 
            MessageName = outbox.EventName
        };
        return sender.SendEventAsync(message);
    }


}