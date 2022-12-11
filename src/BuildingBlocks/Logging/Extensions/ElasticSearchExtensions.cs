using System;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logging.Extensions;
public static class ElasticSearchExtensions
{
    public static void UseElasticApm(this IApplicationBuilder app, IConfiguration configuration)
    {   
        app.UseAllElasticApm(configuration);
    }
}
