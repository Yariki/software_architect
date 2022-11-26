using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;

        public IndexModel(Identity.Api.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<IdentityRole> Roles { get;set; }
        
        public async Task OnGetAsync()
        {
            Roles = await _context.Roles.ToListAsync();
        }
    }
}
