using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;

namespace Identity.Api.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;

        public DeleteModel(Identity.Api.Data.ApplicationDbContext context)
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

            var role  = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (role == null)
            {
                return NotFound();
            }
            RoleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);

            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
