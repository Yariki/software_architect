using CatalogService.Application.Common.Models;
using CatalogService.Application.Product.Commands.AddProduct;
using CatalogService.Application.Product.Commands.DeleteProduct;
using CatalogService.Application.Product.Commands.UpdateProduct;
using CatalogService.Application.Product.Queries.GetProduct;
using CatalogService.Application.Product.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var product = await Mediator.Send(new GetProductQuery { ProductId = id });
        return Ok(product);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetList()
    {
        return Ok(await Mediator.Send(new GetProductsQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddProduct(AddProductCommand cmd)
    {
        return Ok(await Mediator.Send(cmd));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateProductCommand cmd)
    {
        if (id != cmd.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(cmd);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteProductCommand() { Id = id });
        return NoContent();
    }
}
