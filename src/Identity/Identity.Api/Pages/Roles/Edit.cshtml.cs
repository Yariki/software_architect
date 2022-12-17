using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Microsoft.AspNetCore.Identity;
using Identity.Api.Models;
using System.Data.Common;
using Identity.Api.Permissions;

namespace Identity.Api.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(Identity.Api.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private static IList<string> AllPermissions = new List<string>()
            {
                CatalogPermissions.CatalogRead,
                CatalogPermissions.CatalogCreate,
                CatalogPermissions.CatalogUpdate,
                CatalogPermissions.CatalogDelete
            };

        [BindProperty]
        public RoleEditViewModel RoleViewModel { get; set; }

        [BindProperty]
        public List<string> Permissions { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (role == null)
            {
                return NotFound();
            }
            
            var existPermissions = _context.RoleClaims.Where(x => x.RoleId == id).Select(x => x.ClaimValue).ToList();
            
            RoleViewModel = new RoleEditViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Permissions = AllPermissions.Select(permission => new Permission
                {
                    PermissionValue = permission,
                    PermissionLabel = permission,
                    Checked = existPermissions.Contains(permission)
                }).ToList()
            };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {

                var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == RoleViewModel.Id);
                role.Name = RoleViewModel.RoleName;
                role.NormalizedName = RoleViewModel.RoleName.ToUpper();

                var claims = _context.RoleClaims.Where(rc => rc.RoleId == RoleViewModel.Id);

                _context.RoleClaims.RemoveRange(claims);

                foreach (var permission in Permissions)
                {
                    var claim = new IdentityRoleClaim<string>
                    {
                        RoleId = RoleViewModel.Id,
                        ClaimType = CatalogPermissions.Permissions,
                        ClaimValue = permission
                    };

                    _context.RoleClaims.Add(claim);
                }
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleViewModelExists(RoleViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoleViewModelExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
