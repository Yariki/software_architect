using Newtonsoft.Json;

namespace CatalogService.Application.IntegrationTests;

public static class Extensions
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}