using AutoMapper;
using Catalog.Abstractions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Product.Queries.GetProduct;
public class GetProductQuery : IRequest<ProductExtendedDto>
{
    public int ProductId { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductExtendedDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<ProductExtendedDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _applicationDbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);
        return _mapper.Map<ProductExtendedDto>(product);
    }
}
