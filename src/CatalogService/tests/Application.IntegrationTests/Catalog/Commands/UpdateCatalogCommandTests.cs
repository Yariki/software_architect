using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.UpdateCatalog;
using NUnit.Framework;

namespace CatalogService.Application.IntegrationTests.Catalog.Commands;

using static Testing;

public class UpdateCatalogCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireIdAndName()
    {
        var command = new UpdateCatalogCommand();

        await FluentActions.Invoking(() => 
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateName()
    {
        //arrange
        const string updatedName = "UpdatedName";
        
        var cmd = new CreateCatalogCommand() { Name = "TestCatalog" };

        var catalog = await SendAsync(cmd);

        //act
        var update = new UpdateCatalogCommand() { Id = catalog.Id, Name = updatedName };

        var result = await SendAsync(update);

        //assert
        var catalogUpdated = await FindAsync<Domain.Entities.Catalog>(result);
        catalogUpdated.Name.Should().BeEquivalentTo(updatedName);
    }
    
}