using System.Security.Claims;
using Identity.Api.Models;

namespace Identity.Api.Pages.Users;

public class UserViewModel
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }
    
    public string Email { get; set; }

    public string Password { get; set; }

    public static UserViewModel FromApplicationUser(ApplicationUser user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email
        };
    }

    public IList<Claim> Claims { get; set; }

}
