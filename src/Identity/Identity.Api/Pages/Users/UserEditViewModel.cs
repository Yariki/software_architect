using Identity.Api.Models;

namespace Identity.Api.Pages.Users;

public class UserEditViewModel
{
    public string Id { get; set; }

    public string Username { get; set; }
    
    public ICollection<SelectItem> Roles { get; set; }
    
    public static UserEditViewModel FromApplicationUser(ApplicationUser user)
    {
        return new UserEditViewModel
        {
            Id = user.Id,
            Username = user.UserName
        };
    }

}
