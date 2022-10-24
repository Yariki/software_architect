using AutoMapper;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalog;

public class GetCatalogQuery : IRequest<CatalogDto>
{
    public int CatalogId { get; set; }
}

public class GetCatelogQueryHandler : IRequestHandler<GetCatalogQuery, CatalogDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetCatelogQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<CatalogDto> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
    {
        var catalog = await _applicationDbContext.Catalogs.FindAsync(request.CatalogId);

        return _mapper.Map<CatalogDto>(catalog);
    }
}