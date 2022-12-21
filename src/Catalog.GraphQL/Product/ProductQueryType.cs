using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Abstractions;
using CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.GraphQL.Product;

[ExtendObjectType("Query")]
public  class ProductQueryType
{

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    public async Task<IQueryable<CatalogService.Domain.Entities.Product>> GetProductsAsync(
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        return  context.Products;
    }
}
