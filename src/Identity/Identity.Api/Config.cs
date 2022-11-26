using Duende.IdentityServer.Models;

namespace Identity.Api;

public static class Config
{

    public static IEnumerable<ApiResource> ApiResources = new List<ApiResource>()
    {
        new ApiResource("catalog", "Catalog Service")
        {
            Scopes = new string[]
            {
                "catalog.read",
                "catalog.write",
                "catalog.full_access"
            }
        }
    };
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> Scopes = new List<ApiScope>()
    {
        new ApiScope("catalog.read", "Read Access to Catalog API"),
        new ApiScope("catalog.write", "Write Access to Catalog API"),
        new ApiScope("catalog.full_access", "Full Access to Catalog API"),
    };
    
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:5002/signin-oidc" },
               
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "catalog.full_access" }
            },
        };
}
