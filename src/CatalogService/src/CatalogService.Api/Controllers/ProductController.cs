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
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetList([FromQuery] GetProductsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// Create the products
    /// </summary>
    /// <param name="cmd">AddProductCommand - parameters for creating product</param>
    /// <returns>id of product</returns>
    [HttpPost]
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
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, [FromBody]UpdateProductCommand cmd)
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
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteProductCommand() { Id = id });
        return NoContent();
    }
}
