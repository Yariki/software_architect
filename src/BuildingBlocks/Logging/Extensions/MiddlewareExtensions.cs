using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logging.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Logging.Extensions;
public static  class MiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        return app; 
    }

    public static IApplicationBuilder UseApmMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApmMiddleware>();
        return app;
    }
}
