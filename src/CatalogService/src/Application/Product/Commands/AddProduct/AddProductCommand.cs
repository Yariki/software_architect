using CatalogService.Application.Common.Interfaces;
using MediatR;

namespace CatalogService.Application.Product.Commands.AddProduct;

public class AddProductCommand : IRequest<int>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int Image { get; set; }

    public int CatalogId { get; set; }

    public decimal Price { get; set; }

    public uint Amount { get; private set; }
}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public AddProductCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product()
        {
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            CatalogId = request.CatalogId,
            Price = request.Price,
            Amount = request.Amount
        };

        _applicationDbContext.Products.Add(product);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
