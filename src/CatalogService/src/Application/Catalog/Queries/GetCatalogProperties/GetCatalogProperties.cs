using System.Reflection;
using AutoMapper;
using CatalogService.Application.Common.Interfaces;
using CatalogEntity = CatalogService.Domain.Entities.Catalog;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Queries.GetCatalogProperties;

public class GetCatalogPropertiesQuery : IRequest<Dictionary<string,string>>
{
    public int CatalogId { get; set; }
}

public class GetCatalogPropertiesQueryHandler
    : IRequestHandler<GetCatalogPropertiesQuery, Dictionary<string, string>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetCatalogPropertiesQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Dictionary<string, string>> Handle(GetCatalogPropertiesQuery request, 
        CancellationToken cancellationToken)
    {
        var catalog = new CatalogEntity()
        {
            Name = "Test",
            Id = request.CatalogId,
            CatalogId = 0,
            Image = string.Empty
        };

        return new Dictionary<string, string>
            {
                { "Name", catalog.Name },
                { "Id", catalog.Id.ToString() },
                { "CatalogId", catalog.CatalogId.ToString() },
                { "Image", catalog.Image }
            };
    }
}