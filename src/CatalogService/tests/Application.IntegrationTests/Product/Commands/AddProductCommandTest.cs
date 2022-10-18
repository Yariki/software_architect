﻿using System;
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
            };

            var result = await SendAsync(add);

            result.Should().BeGreaterThan(0);

        }
    }
}
