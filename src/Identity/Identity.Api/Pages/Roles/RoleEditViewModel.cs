namespace Identity.Api.Pages.Roles;

public class RoleEditViewModel
{
    public string Id { get; set; }

    public string RoleName { get; set; }

    public List<Permission> Permissions { get; set; }
}

public class Permission
{
    public string PermissionValue { get; set; }
    
    public string PermissionLabel { get; set; }
    
    public bool Checked { get; set; }
}
