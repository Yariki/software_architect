using Catalog.Abstractions;
using Catalog.GraphQL.Models;

namespace Catalog.GraphQL.Product;

[ExtendObjectType("Mutation")]
public class ProductMutationType
{
    
    public async Task<ProductDto> AddProductAsync(ProductDto productDto,
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var product = ProductDto.FromProductDto(productDto);

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        
        return ProductDto.FromProduct(product);
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto productDto,
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var product = context.Products.FirstOrDefault(x => x.Id == productDto.Id);

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.AddAmount(productDto.Amount);

        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);

        return ProductDto.FromProduct(product);
    }

    public async Task<bool> DeleteProduct(int id,
        [Service] IApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var product = context.Products.FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return false;
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }



}
