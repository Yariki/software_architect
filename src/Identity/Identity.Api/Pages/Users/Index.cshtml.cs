using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;

namespace Identity.Api.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;

        public IndexModel(Identity.Api.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserViewModel> UserViewModel { get;set; }

        public async Task OnGetAsync()
        {
            var users = await _context.Users.ToListAsync();

            UserViewModel = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.UserName,
               
            }).ToList();
        }
    }
}
