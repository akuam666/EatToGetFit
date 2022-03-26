using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyAcao
{
    public class DeleteModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public DeleteModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Acao Acao { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Acao = await _context.Acaos.FirstOrDefaultAsync(m => m.Id == id);

            if (Acao == null)
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

            Acao = await _context.Acaos.FindAsync(id);

            if (Acao != null)
            {
                _context.Acaos.Remove(Acao);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
