using System.Runtime.CompilerServices;
using CatalogService.Infrastructure.Persistence;
using HotChocolate.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using System.Text;

namespace CatalogService.Application.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Checkpoint _checkpoint = null!;
    private static string? _currentUserId;

    public static RequestExecutorProxy Executor;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        Executor = _factory.Services.GetRequiredService<RequestExecutorProxy>();

        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" }
        };
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static string? GetCurrentUserId()
    {
        return _currentUserId;
    }

    public static async Task ResetState()
    {
        await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

        _currentUserId = null;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }
    
    public static async Task<IExecutionResult> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        using var result = await Executor.ExecuteAsync(request, cancellationToken);

        result.ExpectQueryResult();

        return result;
    }

    public static async IAsyncEnumerable<string> ExecuteRequestAsStreamAsync(
        Action<IQueryRequestBuilder> configureRequest,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        using var result = await Executor.ExecuteAsync(request, cancellationToken);

        await foreach (var element in result.ExpectResponseStream().ReadResultsAsync().WithCancellation(cancellationToken))
        {
            using (element)
            {
                yield return element.ToJson();
            }
        }
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}
