using System;

public class UrlConfig
{
    public string BasketService { get; set; }

    public string CatalogService { get; set; }

    public class CatalogServiceUrls
    {
        public static string GetCatalogProperties(int id)
        {
            return $"/api/catalog/properties/{id}";
        }
    }

}
