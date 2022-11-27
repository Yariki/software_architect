using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{

    public class CallApiModel : PageModel
    {
        public string Json = string.Empty;

        public async Task OnGet()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var content = await client.GetStringAsync("https://localhost:7265/api/Catalog/identity");

                var parsed = JsonDocument.Parse(content);
                var formatted = JsonSerializer.Serialize(parsed, new JsonSerializerOptions { WriteIndented = true });

                Json = formatted;
            }
            catch (Exception ex)
            {
                Json = ex.ToString();
            }
            
        }
    }
}
