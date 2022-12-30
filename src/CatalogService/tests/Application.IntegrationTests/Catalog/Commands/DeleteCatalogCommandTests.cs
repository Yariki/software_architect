namespace CatalogService.Application.IntegrationTests.Catalog.Commands;

using CatalogService.Application.Common.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.DeleteCatalog;
using NUnit.Framework;
using static Testing;

public class DeleteCatalogCommandTests : BaseTestFixture
{
    [Test]
    public async Task DeleteCatalogThrowEception()
    {
        var cmd = new DeleteCatalogCommand() { Id = 99 };

        await FluentActions.Invoking(() =>
            SendAsync(cmd))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCatalog()
    {
        //arrange
        var cmd = new CreateCatalogCommand() { Name = "Test" };
        var result = await SendAsync(cmd);

        //act
        var delete = new DeleteCatalogCommand() { Id = result };

        await SendAsync(delete);

        //assert
        var catalog = await FindAsync<Domain.Entities.Catalog>(result);

        catalog.Should().BeNull();
    }
}