using CatalogService.Application.Common.Models;
using CatalogService.Application.Product.Commands.AddProduct;
using CatalogService.Application.Product.Commands.DeleteProduct;
using CatalogService.Application.Product.Commands.UpdateProduct;
using CatalogService.Application.Product.Queries.GetProduct;
using CatalogService.Application.Product.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Catalog.Api.Controllers;

public class ProductController : ApiControllerBase
{
    /// <summary>
    /// Returns the filtered list of products. App could filter products by Category and return result in pages. 
    /// </summary>
    /// <param name="query">GetProductsQuery - parameters which include filter and paging</param>
    /// <returns>IEnumerable<ProductDto></returns>
    [HttpGet(Name = nameof(ProductController.GetList))]
    [Produces("application/json")]
    public async Task<ActionResult<EntitiesCollection<ProductDto>>> GetList([FromQuery] GetProductsQuery query)
    {
        var products = await Mediator.Send(query);
        var entitiesCollection = new EntitiesCollection<ProductDto>(products);
        
        UpdateProductCollectionWithLinks(entitiesCollection);
        
        return Ok(entitiesCollection);
    }
    
    /// <summary>
    /// Returns the filtered list of products. App could filter products by Category and return result in pages. 
    /// </summary>
    /// <param name="query">GetProductsQuery - parameters which include filter and paging</param>
    /// <returns>IEnumerable<ProductDto></returns>
    [HttpGet("{id}", Name = nameof(ProductController.GetProduct))]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct(int id)
    {
        var product = await Mediator.Send(new GetProductQuery() { ProductId = id });
        
        UpdateProductWithLinks(product, id);
        
        return Ok(product);
    }

    /// <summary>
    /// Create the products
    /// </summary>
    /// <param name="cmd">AddProductCommand - parameters for creating product</param>
    /// <returns>id of product</returns>
    [HttpPost(Name = nameof(ProductController.AddProduct))]
    public async Task<ActionResult<int>> AddProduct([FromBody]AddProductCommand cmd)
    {
        return Ok(await Mediator.Send(cmd));
    }

    /// <summary>
    /// Update the product
    /// </summary>
    /// <param name="id">int - the id of product</param>
    /// <param name="cmd">UpdateProductCommand - parameters for updating</param>
    /// <returns></returns>
    [HttpPut("{id}", Name = nameof(ProductController.UpdateProduct))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateProduct(int id, [FromBody]UpdateProductCommand cmd)
    {
        if (id != cmd.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(cmd);

        return NoContent();
    }

    /// <summary>
    /// Delete the product
    /// </summary>
    /// <param name="id">int - the id of product</param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = nameof(ProductController.DeleteProduct))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        await Mediator.Send(new DeleteProductCommand() { Id = id });
        return NoContent();
    }
    
    private void UpdateProductWithLinks(ProductExtendedDto productExtendedDto, int id)
    {
        var links = new List<Link>
        {
            new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(GetProduct), values: new { id }),
                "self",
                "GET"),

            new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(DeleteProduct), values: new { id }),
                "delete_product",
                "DELETE"),

            new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(UpdateProduct), values: new { id }),
                "update_product",
                "PUT")
        };

        productExtendedDto.Links.AddRange(links);
    }

    private void UpdateProductCollectionWithLinks<TEntity>(EntitiesCollection<TEntity> entitiesCollection) where TEntity : class
    {
        entitiesCollection.Links.Add(new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(GetProduct), values: new { }),
            "self",
            "GET"));
    }
    
}
