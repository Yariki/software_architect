using Catalog.Abstractions;
using CatalogService.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.DeleteCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.UpdateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalog;

namespace Catalog.GraphQL.Catalog;

[ExtendObjectType("Mutation")]
public class CatalogMutationType
{
    public async Task<CatalogExtendedDto> AddCatalog(
        CreateCatalogCommand createCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) 
    {
        var id = await mediator.Send(createCommand);
        var catalog = await mediator.Send(new GetCatalogQuery() { CatalogId = id}, cancellationToken);

        return catalog;
    }

    public async Task<CatalogExtendedDto> UpdateCatalog(
        UpdateCatalogCommand updateCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var id = await mediator.Send(updateCommand);
        var catalog = await mediator.Send(new GetCatalogQuery() { CatalogId = id }, cancellationToken);

        return catalog;
    }
     
    public async Task<int> DeleteCatalog(DeleteCatalogCommand deleteCatalogCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => 
        await mediator.Send(deleteCatalogCommand);
}
