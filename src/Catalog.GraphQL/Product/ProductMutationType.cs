using AutoMapper;
using Catalog.Abstractions;
using CatalogService.Application.Common.Models;
using CatalogService.Application.Product.Commands.AddProduct;
using CatalogService.Application.Product.Commands.DeleteProduct;
using CatalogService.Application.Product.Commands.UpdateProduct;
using MediatR;

namespace Catalog.GraphQL.Product;

[ExtendObjectType("Mutation")]
public class ProductMutationType
{

    public async Task<ProductDto> AddProductAsync(AddProductCommand addProductCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) =>
        await mediator.Send(addProductCommand);

    public async Task<ProductDto> UpdateProductAsync(
        UpdateProductCommand updateProductCommand,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) =>
        await mediator.Send(updateProductCommand);

    public async Task<int> DeleteProduct(int id,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteProductCommand { Id = id });

}
