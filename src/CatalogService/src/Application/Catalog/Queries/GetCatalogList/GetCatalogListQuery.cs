using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalog.Abstractions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalogList;

public record GetCatalogListQuery : IRequest<IEnumerable<CatalogDto>>;

public class GetCatalogListQueryHandler : IRequestHandler<GetCatalogListQuery, IEnumerable<CatalogDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetCatalogListQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CatalogDto>> Handle(GetCatalogListQuery request, CancellationToken cancellationToken)
    {
        var catalogs = await _applicationDbContext
            .Catalogs
            .AsNoTracking()
            .ProjectTo<CatalogDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return catalogs;
    }
}