using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Pages.Users
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

        [BindProperty]
        public UserEditViewModel UserViewModel { get; set; }

        [BindProperty]
        public List<string> SelectedRoles { get; set; }
        

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel = UserEditViewModel.FromApplicationUser(user);
            var roles = await _context.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            UserViewModel.Roles = roles.Select(role => new SelectItem
            {
                Id = role.Id,
                RoleName = role.Name,
                Checked = userRoles.Contains(role.Name)
            }).ToList();
            

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

            var user = await _userManager.FindByIdAsync(UserViewModel.Id);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            await _userManager.AddToRolesAsync(user, SelectedRoles);
            
            return RedirectToPage("./Index");
        }
        
    }
}
