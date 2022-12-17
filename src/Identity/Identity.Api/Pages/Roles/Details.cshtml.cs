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
    public class DetailsModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;

        public DetailsModel(Identity.Api.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RoleViewModel RoleViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);
            RoleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            if (RoleViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
