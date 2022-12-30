using AutoMapper;
using Catalog.Abstractions;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Commands.UpdateCatalog;

public class UpdateCatalogCommand : IRequest<int>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public int? CatalogId { get; set; }
}


public class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommand, int>
{
    private IApplicationDbContext _applicationDbContext;
    private IMapper _mapper;

    public UpdateCatalogCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = _applicationDbContext.Catalogs.Find(request.Id);

        if (catalog == null)
        {
            throw new NotFoundException(nameof(Catalog), request.Id);
        }

        catalog.Name = request.Name;
        catalog.Image = request.Image;
        catalog.CatalogId = request.CatalogId;

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return catalog.Id;
    }
}