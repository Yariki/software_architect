using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Logging.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Logging.Extensions;
public static class LoggingExtensions
{
    public static WebApplicationBuilder AddSerialLogging(this WebApplicationBuilder builder, 
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithExceptionDetails()
            .Enrich.WithElasticApmCorrelationInfo()
            .Enrich.WithProperty("ApplicationName", configuration["ApplicationName"])
            .WriteTo.Async(writeTo => writeTo
                .Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticsearchSettings:uri"]))
                {
                    CustomFormatter = new EcsTextFormatter(),
                    AutoRegisterTemplate = true,
                    IndexFormat = configuration["ElasticsearchSettings:defaultIndex"],
                    ModifyConnectionSettings = x => x.BasicAuthentication(configuration["ElasticsearchSettings:username"], configuration["ElasticsearchSettings:password"])
                })
            )
            .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(Log.Logger, true);

        builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();

        builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        builder.Services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

        return builder;
    }
}
