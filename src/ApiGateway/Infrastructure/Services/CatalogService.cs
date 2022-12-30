using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ApiGateway.Infrastructure.Services;

public interface ICatalogService
{
    Task<Dictionary<string, string>> GetCatalogProperties(int id);
}

public class CatalogService : ICatalogService
{
    private readonly UrlConfig _urlConfig;
    private readonly HttpClient _httpClient;
   
    public CatalogService(HttpClient httpClient,  IOptions<UrlConfig> urlConfig)
    {
        _urlConfig = urlConfig.Value;
        _httpClient = httpClient;
    }
    
    public async Task<Dictionary<string,string>> GetCatalogProperties(int id)
    {
        var url = $"{_urlConfig.CatalogService}{UrlConfig.CatalogServiceUrls.GetCatalogProperties(id)}";
        var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var value = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<Dictionary<string, string>>(value);

        return result;

    }



}
