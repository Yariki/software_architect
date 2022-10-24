using CatalogService.Application.Common.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;
using NUnit.Framework;

namespace CatalogService.Application.IntegrationTests.Catalog.Commands;

using static Testing;

public class CreateCatalogCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireName()
    {
        var command = new CreateCatalogCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCatalog()
    {
        const string name = "Test Catalog";

        var command = new CreateCatalogCommand() { Name = name };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.Name.Should().BeEquivalentTo(name);
    }

}