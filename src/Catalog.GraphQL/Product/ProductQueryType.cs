using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Abstractions;
using Catalog.GraphQL.Models;
using CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.GraphQL.Product;

[ExtendObjectType("Query")]
public  class ProductQueryType
{

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    public async Task<IEnumerable<ProductDto>> GetProductsAsync(
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var products = await context.Products.ToListAsync(cancellationToken);

        return products.Select(ProductDto.FromProduct);
    }
}
