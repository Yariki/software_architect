using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Product.Commands.AddProduct;
using CatalogService.Application.Product.Commands.DeleteProduct;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CatalogService.Application.IntegrationTests.Product.Commands;

using static Testing;

public class DeleteProductCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowExceptionWhileDeletingProduct()
    {
        var delete = new DeleteProductCommand() { Id = 99 };

        await FluentActions
            .Invoking(() => SendAsync(delete))
            .Should()
            .ThrowAsync<NotFoundException>();

    }

    [Test]
    public async Task ShouldDeleteProduct()
    {
        var add = new AddProductCommand() { Name = "Product", CatalogId = 1, Price = 10 };
        var productId = await SendAsync(add);


        var delete = new DeleteProductCommand() { Id = productId };
        await SendAsync(delete);

        var product = await FindAsync<Domain.Entities.Product>(productId);
        product.Should().BeNull();
    }
}