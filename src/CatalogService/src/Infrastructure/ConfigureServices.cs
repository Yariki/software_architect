using CatalogService.Application.Common.Interfaces;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Infrastructure.Services;
using EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CatalogServiceDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddTransient<IDateTime, DateTimeService>();
        //services.Configure<AzureServiceBusProducerConfiguration>(configuration.GetSection("ServiceBus"));
        //services.AddScoped<IEventSender, EventSender>();
        //services.AddHostedService<ProducerService>();

        return services;
    }
}
