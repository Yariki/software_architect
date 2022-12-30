using AutoMapper;
using Catalog.Abstractions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalog;

public class GetCatalogQuery : IRequest<CatalogExtendedDto>
{
    public int CatalogId { get; set; }
}

public class GetCatelogQueryHandler : IRequestHandler<GetCatalogQuery, CatalogExtendedDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetCatelogQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<CatalogExtendedDto> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
    {
        var catalog = await _applicationDbContext
            .Catalogs
            .FindAsync(request.CatalogId);

        return _mapper.Map<CatalogExtendedDto>(catalog);
    }
}