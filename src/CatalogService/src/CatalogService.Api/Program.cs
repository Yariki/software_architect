using Catalog.Api.Extensions;
using Catalog.GraphQL;
using Logging.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerialLogging(builder.Configuration);

Log.Information("Starting Catalog API...");

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity();
builder.Services.AddGraphQlFunctionality();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();

public partial class Program { }
