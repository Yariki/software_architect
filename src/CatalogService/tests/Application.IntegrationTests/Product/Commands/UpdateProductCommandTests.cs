using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Product.Commands.AddProduct;
using CatalogService.Application.Product.Commands.UpdateProduct;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;

namespace CatalogService.Application.IntegrationTests.Product.Commands;
using static Testing; 

public class UpdateProductCommandTests : BaseTestFixture
{
    [Test]
    public async Task UpdateProductNameIdRequired()
    {
        var update = new UpdateProductCommand();

        await FluentActions
            .Invoking(() => SendAsync(update))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ProductShouldBeUpdated()
    {
        const string updatedName = "UpdatedName";
        
        //arrange
        var add = new AddProductCommand() { Name = "Product", CatalogId = 1 };
        var productId = await SendAsync(add);
        
        //act
        var update = new UpdateProductCommand() { Name = updatedName, CatalogId = 1, Id = productId, Amount = 10 };
        productId = await SendAsync(update);
        
        //assert
        var product = await FindAsync<Domain.Entities.Product>(productId);
        product.Should().NotBeNull();
        product.Name.Should().Be(updatedName);
        product.Amount.Should().Be(10);
    }
}