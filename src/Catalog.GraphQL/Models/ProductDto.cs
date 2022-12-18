using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.GraphQL.Models;
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public int Image { get; set; }

    public int CatalogId { get; set; }
    
    public decimal Price { get; set; }

    public uint Amount { get; set; }

    public static ProductDto FromProduct(CatalogService.Domain.Entities.Product product)
    {
        return new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Image = product.Image,
            CatalogId = product.CatalogId,
            Price = product.Price,
            Amount = product.Amount
        };
    }

    public static CatalogService.Domain.Entities.Product FromProductDto(ProductDto productDto)
    {
        var product = new CatalogService.Domain.Entities.Product()
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            Image = productDto.Image,
            CatalogId = productDto.CatalogId,
            Price = productDto.Price,
            
        };
        product.AddAmount(productDto.Amount);
        return product;
    }
}
