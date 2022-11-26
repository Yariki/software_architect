using System.Security.Claims;
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Identity.Api.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api;

public class IdentityProfileService : ProfileService<ApplicationUser>
{
    public IdentityProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory) 
        : base(userManager, claimsFactory)
    {
    }

    protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
    {
        var principal = await GetUserClaimsAsync(user);
        var roles = await UserManager.GetRolesAsync(user);

        var claims = new List<Claim>();

        foreach (var item in roles)
        {
            claims.Add(new Claim(JwtClaimTypes.Role, item));
        }

        context.IssuedClaims.AddRange(claims);
        context.AddRequestedClaims(principal.Claims);
    }
}
