using CatalogService.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.DeleteCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.UpdateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalogList;

namespace Catalog.Api.Controllers;


public class CatalogController : ApiControllerBase
{

    public CatalogController()
    {
    }

    [HttpGet]
    [Route("catalogs")]
    public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogItems()
    {
        return Ok(await Mediator.Send(new GetCatalogListQuery()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<CatalogDto>> GetCatalog(int id)
    {
        return Ok(await Mediator.Send(new GetCatalogQuery() { CatalogId = id }));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCatalogCommand cmd)
    {
        return Ok(await Mediator.Send(cmd));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateCatalogCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCatalogCommand() { Id = id });

        return NoContent();
    }
}
