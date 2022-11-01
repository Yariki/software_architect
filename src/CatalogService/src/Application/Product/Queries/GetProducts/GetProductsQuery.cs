using AutoMapper;
using AutoMapper.QueryableExtensions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Product.Queries.GetProducts;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    internal const int DefaultPageSize = 20; // could be set in configuration
    
    public int? CategoryId { get; set; }
    
    public int? Page { get; set; }
    
    public int? PageSize { get; set; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await FilterFunction(request)
            .AsNoTracking()
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return products;

        IQueryable<Domain.Entities.Product> FilterFunction(GetProductsQuery query)
        {
            int page = query.Page.HasValue ? query.Page.Value : 1;
            int size = query.PageSize.HasValue ? query.PageSize.Value : GetProductsQuery.DefaultPageSize;

            var filteredProducts = query.CategoryId.HasValue
                ? _applicationDbContext.Products.Where(p => p.CatalogId == query.CategoryId)
                : _applicationDbContext.Products;
            return filteredProducts.Skip((page - 1) * size).Take(size);
        }
        
    }
}