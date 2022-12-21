using AutoMapper;
using Catalog.Abstractions;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using CatalogService.Domain.Events;
using MediatR;

namespace CatalogService.Application.Product.Commands.UpdateProduct;
public class UpdateProductCommand : IRequest<ProductDto>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Image { get; set; }

    public int CatalogId { get; set; }

    public decimal Price { get; set; }

    public uint Amount { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IApplicationDbContext applicationDbContext,
        IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _applicationDbContext.Products.FindAsync(request.Id);

        if (product == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
        }


        product.Name = request.Name;
        product.Description = request.Description;
        product.Image = request.Image;
        product.CatalogId = request.CatalogId;
        product.Price = request.Price;
        product.AddAmount(request.Amount);
        product.AddDomainEvent(new UpdatedProductEvent(product));

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }
}