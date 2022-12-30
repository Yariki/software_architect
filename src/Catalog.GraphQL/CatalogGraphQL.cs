using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.GraphQL.Catalog;
using Catalog.GraphQL.Product;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.GraphQL;
public static  class CatalogGraphQL
{

    public static IServiceCollection AddGraphQlFunctionality(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddFiltering()
            .AddQueryType(d => d.Name("Query"))
            .AddType<CatalogQueryType>()
            .AddType<ProductQueryType>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddType<CatalogMutationType>()
            .AddType<ProductMutationType>()
            .AddProjections()
            .BindRuntimeType<int, IntType>()
            .BindRuntimeType<string, StringType>()
            .BindRuntimeType<uint, IntType>()
            .BindRuntimeType<decimal, DecimalType>();

        return services;
    }
}
