using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodProject.Data;
using FoodProject.Data.Ententies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FoodProject.Pages.MyBlacklist
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(FoodProject.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["AlimentoId"] = new SelectList(_context.Alimentos, "Id", "Name");
        
            return Page();
        }

        [BindProperty]
        public Blacklist Blacklist { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            Blacklist.UserId = userId;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Blacklist.Add(Blacklist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
