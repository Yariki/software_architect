using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;

namespace Identity.Api.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;

        public EditModel(Identity.Api.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoleViewModel RoleViewModel { get; set; }

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
            
            RoleViewModel = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
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

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == RoleViewModel.Id);
            role.Name = RoleViewModel.RoleName;
            role.NormalizedName = RoleViewModel.RoleName.ToUpper();

            try
            {
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
