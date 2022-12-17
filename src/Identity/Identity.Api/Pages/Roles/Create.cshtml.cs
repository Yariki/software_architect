using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Identity.Api.Data;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;

        public CreateModel(Identity.Api.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RoleViewModel RoleViewModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var role = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = RoleViewModel.RoleName,
                NormalizedName = RoleViewModel.RoleName.ToUpper()
            };
            
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
