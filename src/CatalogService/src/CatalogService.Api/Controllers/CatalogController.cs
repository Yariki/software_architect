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
    [HttpGet(Name = nameof(CatalogController.GetCatalogItems))]
    [Produces("application/json")]
    public async Task<ActionResult<EntitiesCollection<CatalogDto>>> GetCatalogItems()
    {
        var catalogs = await Mediator.Send(new GetCatalogListQuery());
        var entitiesCollection = new EntitiesCollection<CatalogDto>(catalogs);
        
        UpdateCatalogCollectionWithLinks(entitiesCollection);
        
        return Ok(entitiesCollection);
    }
    
    /// <summary>
    /// get list of catalogs
    /// </summary>
    /// <returns>IEnumerable<CatalogDto></returns>
    [HttpGet("{id}", Name = nameof(CatalogController.GetCatalog))]
    [Produces("application/json")]
    public async Task<ActionResult<CatalogDto>> GetCatalog(int id)
    {
        var catalog = await Mediator.Send(new GetCatalogQuery { CatalogId = id });
        
        return Ok(catalog);
    }
    
    /// <summary>
    /// Create the catalog
    /// </summary>
    /// <param name="cmd">CreateCatalogCommand - Input parameters</param>
    /// <returns>int - id of created catalog</returns>
    [HttpPost(Name = nameof(CatalogController.CreateCatalog))]
    public async Task<ActionResult<int>> CreateCatalog([FromBody] CreateCatalogCommand cmd)
    {
        return Ok(await Mediator.Send(cmd));
    }

    /// <summary>
    /// Updated the catalog
    /// </summary>
    /// <param name="id">int - the id of Catalog</param>
    /// <param name="command">UpdateCatalogCommand - parameters which app needs to update</param>
    /// <returns></returns>
    [HttpPut("{id}", Name = nameof(CatalogController.UpdateCatalog))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateCatalog(int id, [FromBody] UpdateCatalogCommand command)
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
    [HttpDelete("{id}", Name = nameof(CatalogController.DeleteCatalog))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteCatalog(int id)
    {
        await Mediator.Send(new DeleteCatalogCommand() { Id = id });

        return NoContent();
    }

    private void UpdateCatalogWithLinks(CatalogExtendedDto catalogExtendedDto, int id)
    {
        var links = new List<Link>
        {
            new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(GetCatalog), values: new { id }),
                "self",
                "GET"),

            new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(DeleteCatalog), values: new { id }),
                "delete_catalog",
                "DELETE"),

            new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(UpdateCatalog), values: new { id }),
                "update_caltalog",
                "PUT")
        };

        catalogExtendedDto.Links.AddRange(links);
    }

    private void UpdateCatalogCollectionWithLinks<TEntity>(EntitiesCollection<TEntity> entitiesCollection) where TEntity : class
    {
        entitiesCollection.Links.Add(new Link(LinkGenerator.GetUriByAction(HttpContext, nameof(GetCatalogItems), values: new { }),
            "self",
            "GET"));
    }
}
