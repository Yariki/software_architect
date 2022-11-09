using EventBus;

namespace CartingService.Infrastructure.Services;

public class ReceiverHostedService : BackgroundService
{

    private readonly IEventReceiver _eventReceiver;
    private readonly ILogger<ReceiverHostedService> _logger;

    public ReceiverHostedService(IEventReceiver eventReceiver, ILogger<ReceiverHostedService> logger)
    {
        _eventReceiver = eventReceiver;
        _logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await _eventReceiver.StartReceiveMessageAsync();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _eventReceiver.DisposeAsync();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
