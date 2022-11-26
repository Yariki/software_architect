using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityModel;

namespace Identity.Api.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Identity.Api.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(Identity.Api.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserViewModel UserViewModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = new ApplicationUser
            {
                UserName = UserViewModel.Username,
                Email = UserViewModel.Email,
                
            };
            var result = await _userManager.CreateAsync(user, UserViewModel.Password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = _userManager.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Name, $"{UserViewModel.FirstName} {UserViewModel.LastName}"),
                            new Claim(JwtClaimTypes.GivenName, UserViewModel.FirstName),
                            new Claim(JwtClaimTypes.FamilyName, UserViewModel.LastName)
                        }).Result;


            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return RedirectToPage("./Index");
        }
    }
}
