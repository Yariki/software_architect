using Identity.Api;
using Logging.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddSerialLogging(builder.Configuration);

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.UseElasticApm(builder.Configuration);

    app.UseCorrelationIdMiddleware();
    app.UseApmMiddleware();

    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    if (args.Contains("/seed"))
    {
        Log.Information("Seeding database...");
        SeedData.EnsureSeedData(app);
        Log.Information("Done seeding database. Exiting.");
        return;
    }

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}