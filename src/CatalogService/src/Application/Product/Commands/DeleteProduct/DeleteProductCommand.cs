using Catalog.Abstractions;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using MediatR;

namespace CatalogService.Application.Product.Commands.DeleteProduct;
public class 
    DeleteProductCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteProductCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _applicationDbContext.Products.FindAsync(request.Id);

        if (product == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
        }

        _applicationDbContext.Products.Remove(product);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
