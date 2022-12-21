using System;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Product.Commands.AddProduct;
using FluentAssertions;
using NUnit.Framework;
using static CatalogService.Application.IntegrationTests.Testing;
namespace CatalogService.Application.IntegrationTests.Product.Commands
{
    public class AddProductCommandTest : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireName()
        {
            var add = new AddProductCommand();

            await FluentActions
                .Invoking(() => SendAsync(add))
                .Should()
                .ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateProduct()
        {

            var add = new AddProductCommand()
            {
                Name = "Product",
                CatalogId = 1,
                Amount = 10
            };

            var result = await SendAsync(add);

            result.Should().NotBeNull();
            
        }
    }
}

