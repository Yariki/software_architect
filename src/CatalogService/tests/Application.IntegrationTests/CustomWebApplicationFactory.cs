using Catalog.Abstractions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Infrastructure.Persistence;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Application.IntegrationTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services.Remove<ApplicationDbContext>();
            services.Remove<IApplicationDbContext>();

            services
               .Remove<DbContextOptions<ApplicationDbContext>>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "CatalogServiceDb"));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton(sp => new RequestExecutorProxy(
                sp.GetRequiredService<IRequestExecutorResolver>(),
                Schema.DefaultName
            ));

        });
    }
}
