using Duende.IdentityServer.Models;

using Identity.Api.Permissions;

namespace Identity.Api;

public static class Config
{

    public readonly static IEnumerable<ApiResource> ApiResources = new List<ApiResource>()
    {
        new ApiResource("catalog", "Catalog Service")
        {
            Scopes = new string[]
            {
                "catalog"
            }
        }
    };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = CatalogPermissions.Permissions,
                DisplayName = CatalogPermissions.Permissions,
                UserClaims = {CatalogPermissions.Permissions}
            }
        };

    public readonly static IEnumerable<ApiScope> Scopes = new List<ApiScope>()
    {
        new ApiScope("catalog")
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
                AllowedScopes =
                {
                    "openid",
                    "profile",
                    "catalog",
                    CatalogPermissions.Permissions
                }
            },
        };
}
