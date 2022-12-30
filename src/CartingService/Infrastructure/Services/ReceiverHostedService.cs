using EventBus;

namespace CartingService.Infrastructure.Services;

public class ReceiverHostedService : BackgroundService
{

    private readonly IEventReceiver _eventReceiver;

    public ReceiverHostedService(IEventReceiver eventReceiver)
    {
        _eventReceiver = eventReceiver;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await _eventReceiver.StartReceiveMessageAsync().ConfigureAwait(false);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _eventReceiver.DisposeAsync().ConfigureAwait(false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
