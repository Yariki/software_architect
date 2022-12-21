using Catalog.Abstractions;
using CatalogService.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.DeleteCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.UpdateCatalog;

namespace Catalog.GraphQL.Catalog;

[ExtendObjectType("Mutation")]
public class CatalogMutationType
{
    public async Task<CatalogDto> AddCatalog(
        CreateCatalogCommand createCommand, 
        [Service] IMediator mediator,
        CancellationToken cancellationToken) =>
         await mediator.Send(createCommand);
        

    public async Task<CatalogDto> UpdateCatalog(
        UpdateCatalogCommand updateCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => 
         await mediator.Send(updateCommand);

    public async Task<int> DeleteCatalog(DeleteCatalogCommand deleteCatalogCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => 
        await mediator.Send(deleteCatalogCommand);
}
