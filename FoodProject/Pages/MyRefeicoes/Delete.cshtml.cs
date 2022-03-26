using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyRefeicoes
{
    public class DeleteModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public DeleteModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Refeicao Refeicao { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Refeicao = await _context.Refeicaos
                .Include(r => r.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Refeicao == null)
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

            Refeicao = await _context.Refeicaos.FindAsync(id);

            if (Refeicao != null)
            {
                _context.Refeicaos.Remove(Refeicao);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
