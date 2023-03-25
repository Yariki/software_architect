using AutoMapper;

using Catalog.Abstractions;

using MediatR;

namespace CatalogService.Application.Product.Commands.AddProduct;

public class AddProductCommand : IRequest<int>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int Image { get; set; }

    public int CatalogId { get; set; }

    public decimal Price { get; set; }

    public uint Amount { get; set; }
}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public AddProductCommandHandler(IApplicationDbContext applicationDbContext,
        IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product(request.Amount)
        {
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            CatalogId = request.CatalogId,
            Price = request.Price
        };

        _applicationDbContext.Products.Add(product);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
