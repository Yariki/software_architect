using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Configuration;
using Serilog.Context;
using Serilog.Core;

namespace Logging.Middleware;
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
   
    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        var correlationId = GetCorrelationId(context, correlationIdGenerator);
        AddCorrelationIdHeaderToResponse(context, correlationId);

        using var correlationIdProperty = LogContext.PushProperty(LoggingConsts.CorrelationIdProperty, correlationId);
        await _next(context);
    }

    private void AddCorrelationIdHeaderToResponse(HttpContext context, string correlationId)
    {
        context.Response.OnStarting(() =>
        {
            if (!context.Response.Headers.ContainsKey(LoggingConsts.CorrelationHeader))
            {
                context.Response.Headers.TryAdd(LoggingConsts.CorrelationHeader, new[] { correlationId });
            }
            
            return Task.CompletedTask;
        });
    }

    public string GetCorrelationId(HttpContext httpContext, ICorrelationIdGenerator correlationIdGenerator)
    {
        if (httpContext.Request.Headers.ContainsKey(LoggingConsts.CorrelationHeader))
        {
            var correlationId = httpContext.Request.Headers[LoggingConsts.CorrelationHeader];
            correlationIdGenerator.SetCorrelaltionId(correlationId);
            return correlationId;
        }
        return correlationIdGenerator.CorrelationId;
    }

}
