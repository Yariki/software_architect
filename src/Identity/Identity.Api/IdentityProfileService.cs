using System.Security.Claims;
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Identity.Api.Data;
using Identity.Api.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api;

public class IdentityProfileService : ProfileService<ApplicationUser>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public IdentityProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        ApplicationDbContext applicationDbContext) 
        : base(userManager, claimsFactory)
    {
        _applicationDbContext = applicationDbContext;
    }

    protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
    {
        var principal = await GetUserClaimsAsync(user);
        var roles = await UserManager.GetRolesAsync(user);
        

        var claims = new List<Claim>();

        foreach (var item in roles)
        {
            claims.Add(new Claim(JwtClaimTypes.Role, item));
            var role = _applicationDbContext.Roles.FirstOrDefault(r => r.Name == item);
            var roleClaims = await _applicationDbContext.RoleClaims.Where(c => c.RoleId == role.Id).ToListAsync();

            foreach (var rc in roleClaims)
            {
                claims.Add(new Claim(rc.ClaimType, rc.ClaimValue));
            }
            
        }

        context.IssuedClaims.AddRange(claims);
        context.AddRequestedClaims(principal.Claims);
    }
}
