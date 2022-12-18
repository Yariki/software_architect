using Catalog.GraphQL.Catalog;
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
                @"mutation($catalogInput: CatalogInput!) {
                        addCatalog(catalogInput: $catalogInput) {
                            id
                        }
                    }")
            .SetVariableValue("catalogInput", new CatalogInput()
            {
                Id = 0,
                CatalogId = 0,
                Name = "Test",
                Image = "sdkfj"
            }));
        
        result.MatchSnapshot();
    }
    
}