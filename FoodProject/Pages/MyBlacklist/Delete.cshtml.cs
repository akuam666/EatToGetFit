using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyBlacklist
{
    public class DeleteModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public DeleteModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Blacklist Blacklist { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blacklist = await _context.Blacklist
                .Include(b => b.Alimentos)
                .Include(b => b.User).FirstOrDefaultAsync(m => m.id == id);

            if (Blacklist == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blacklist = await _context.Blacklist.FindAsync(id);

            if (Blacklist != null)
            {
                _context.Blacklist.Remove(Blacklist);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
