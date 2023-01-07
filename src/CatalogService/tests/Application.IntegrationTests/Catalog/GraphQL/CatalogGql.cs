using Catalog.GraphQL.Catalog;
using Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;

using NUnit.Framework;
using Snapshooter.NUnit;

namespace CatalogService.Application.IntegrationTests.Catalog.GraphQL;

public class CatalogGql : BaseTestFixture
{
    
    [Test]
    public async Task GetCatalogs_Success()
    {
        var result = await Testing.ExecuteRequestAsync(
            b => b.SetQuery("{ catalogs { nodes {id} } }"));

        result.MatchSnapshot();
    }

    [Test]
    public async Task CreateCatalog_Success()
    {
        var result = await Testing.ExecuteRequestAsync(b => b.SetQuery(
                @"mutation($createCommand: CreateCatalogCommand!) {
                        addCatalog(createCommand: $createCommand) {
                            id
                        }
                    }")
            .SetVariableValue("catalogInput", new CreateCatalogCommand()
            {   
                CatalogId = 0,
                Name = "Test",
                Image = "sdkfj"
            }));
        
        result.MatchSnapshot();
    }
    
}