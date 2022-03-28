using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyFavoritos
{
    public class DetailsModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public DetailsModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Favoritos Favoritos { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favoritos = await _context.Favoritos
                .Include(f => f.Alimentos)
                .Include(f => f.User).FirstOrDefaultAsync(m => m.id == id);

            if (Favoritos == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
