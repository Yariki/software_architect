using AutoMapper;
using Catalog.Abstractions;
using CatalogService.Application.Common.Models;
using CatalogService.Application.Product.Commands.AddProduct;
using CatalogService.Application.Product.Commands.DeleteProduct;
using CatalogService.Application.Product.Commands.UpdateProduct;
using CatalogService.Application.Product.Queries.GetProduct;
using MediatR;

namespace Catalog.GraphQL.Product;

[ExtendObjectType("Mutation")]
public class ProductMutationType
{

    public async Task<ProductExtendedDto> AddProductAsync(AddProductCommand addProductCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var id = await mediator.Send(addProductCommand);
        var product = await mediator.Send(new GetProductQuery() { ProductId = id }, cancellationToken);
        return product;
    }


    public async Task<ProductExtendedDto> UpdateProductAsync(
        UpdateProductCommand updateProductCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var id = await mediator.Send(updateProductCommand);
        var product = await mediator.Send(new GetProductQuery() { ProductId = id }, cancellationToken);
        return product;
    }

    public async Task<int> DeleteProduct(int id,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteProductCommand { Id = id });

}
