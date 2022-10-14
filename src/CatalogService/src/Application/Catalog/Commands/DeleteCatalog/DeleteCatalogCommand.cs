using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Commands.DeleteCatalog;

public class DeleteCatalogCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteCatalogCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = await _applicationDbContext.Catalogs.FindAsync(request.Id);

        if (catalog == null)
        {
            throw new NotFoundException(nameof(Catalog), request.Id);
        }

        _applicationDbContext.Catalogs.Remove(catalog);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return catalog.Id;
    }
}