using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Enteties;

namespace FoodProject.Pages.MyAlimentos
{
    public class DetailsModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public DetailsModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Alimento Alimento { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Alimento = await _context.Alimentos
                .Include(a => a.Categoria)
                //.Include(a => a.AlimentoAcao) // tabela intermedia
                //.ThenInclude(s => s.Acao)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Alimento == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
