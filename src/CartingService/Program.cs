using CartingService.Application.Interfaces;
using CartingService.Infrastructure.Filters;
using CartingService.Infrastructure.Persistance;

using MediatR;

using System.Reflection;
using CartingService.Infrastructure;
using CartingService.Infrastructure.Filters;
using CartingService.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using EventBus;
using CartingService.Application.MessageHandlers;
using CartingService.Infrastructure.Services;
using Logging.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerialLogging(builder.Configuration);

Log.Information("Starting Cart API...");

// Add services to the container.

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<CartingService.Infrastructure.Persistance.IApplicationDbContext, ApplicationDbContext>();
//builder.Services.Configure<AzureServiceBusProducerConfiguration>(builder.Configuration.GetSection("ServicecBus"));
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IInboxRepository, InboxRepository>();
builder.Services.AddTransient<IMessageHandler, UpdatedProductMessageHandler>();
builder.Services.AddSingleton<IEventReceiver, EventReceiver>();
//builder.Services.AddHostedService<ReceiverHostedService>();
builder.Services.AddControllers(opt => 
{
    opt.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new QueryStringApiVersionReader("x-api-version"));
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

CheckBusSettings(builder);

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseElasticApm(builder.Configuration);

app.UseCorrelationIdMiddleware();
app.UseApmMiddleware();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program 
{ 

    private static void CheckBusSettings(WebApplicationBuilder builder)
    {

        using var provider = builder
            .Services
            .BuildServiceProvider();

        using (var scope = provider.CreateScope())
        {
            var options = scope.ServiceProvider.GetRequiredService<IOptions<AzureServiceBusProducerConfiguration>>();
            Console.WriteLine(options.Value.ConnectionString);
        } 
    }

}


