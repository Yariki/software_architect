using AutoFixture;

namespace CartingService.UnitTests;

public class TestBase
{
    protected readonly CancellationTokenSource _cancellationTokenSource;
    protected readonly Fixture _fixture;

    public TestBase()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _fixture = new Fixture();
    }
}