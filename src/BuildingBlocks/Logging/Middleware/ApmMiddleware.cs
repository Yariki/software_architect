using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Elastic.Apm;
using Logging.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Logging.Middleware;
public class ApmMiddleware
{
    private readonly RequestDelegate _next;

    public ApmMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task Invoke(HttpContext context, IConfiguration configuration)
    {
        var applicationName = configuration["ApplicationName"];

        var apmTransation = Agent.Tracer.StartTransaction($"{applicationName}_{LoggingConsts.ApmTransaction}", $"{applicationName}");

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            apmTransation?.CaptureException(ex);
        }
        finally
        {
            apmTransation?.End();
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Log.Error(exception, exception.Message);
        var code = HttpStatusCode.InternalServerError;

        var result = System.Text.Json.JsonSerializer.Serialize(new { error = exception?.Message });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
