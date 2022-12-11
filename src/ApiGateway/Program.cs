using ApiGateway.Infrastructure.Services;
using Elastic.Apm.AspNetCore;
using Logging.Extensions;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerialLogging(builder.Configuration);
    
Log.Information("Starting API Gateway...");

builder.Configuration.AddJsonFile("ocelot.json");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.Configure<UrlConfig>(builder.Configuration.GetSection("Urls"));
builder.Services.AddTransient<ICatalogService, CatalogService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("AuthTest", options =>
            {
                options.Authority = "https://localhost:5001";
                options.TokenValidationParameters.ValidateAudience = false;
            });

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseElasticApm(builder.Configuration);

app.UseCorrelationIdMiddleware();
app.UseApmMiddleware();

app.UseHttpsRedirection();

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
