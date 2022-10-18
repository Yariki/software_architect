using CatalogService.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.DeleteCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.UpdateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalogList;
using Microsoft.AspNetCore.Http;

namespace Catalog.Api.Controllers;


public class CatalogController : ApiControllerBase
{

    public CatalogController()
    {
    }

    /// <summary>
    /// get list of catalogs
    /// </summary>
    /// <returns>IEnumerable<CatalogDto></returns>
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogItems()
    {
        return Ok(await Mediator.Send(new GetCatalogListQuery()));
    }

    /// <summary>
    /// Create the catalog
    /// </summary>
    /// <param name="cmd">CreateCatalogCommand - Input parameters</param>
    /// <returns>int - id of created catalog</returns>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateCatalogCommand cmd)
    {
        return Ok(await Mediator.Send(cmd));
    }

    /// <summary>
    /// Updated the catalog
    /// </summary>
    /// <param name="id">int - the id of Catalog</param>
    /// <param name="command">UpdateCatalogCommand - parameters which app needs to update</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCatalogCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Delete the catalog
    /// </summary>
    /// <param name="id">int - the id od catalog</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCatalogCommand() { Id = id });

        return NoContent();
    }
}
