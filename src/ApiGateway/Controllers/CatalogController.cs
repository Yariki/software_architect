using ApiGateway.Infrastructure.Services;
using ApiGateway.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ICatalogService catalogService, ILogger<CatalogController> logger)
    {
        _catalogService = catalogService;
        _logger = logger;
    }


    [HttpGet("properties/{id}")]
    public async Task<CatalogDto> GetProperties(int id)
    {
        var dictionary = await _catalogService.GetCatalogProperties(id);

        return MapToCatalogDto(dictionary);
    }
    
    private CatalogDto MapToCatalogDto(Dictionary<string, string> dictionary)
    {
        var catalog = new CatalogDto();
        var properties = typeof(CatalogDto).GetProperties();

        foreach (var property in properties)
        {
            if (!dictionary.ContainsKey(property.Name))
            {
                continue;
            }
            property.SetValue(catalog, dictionary[property.Name]);
        }
        return catalog;
    }
}

