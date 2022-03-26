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
    public class DeleteModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public DeleteModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Alimento = await _context.Alimentos.FindAsync(id);

            if (Alimento != null)
            {
                _context.Alimentos.Remove(Alimento);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
