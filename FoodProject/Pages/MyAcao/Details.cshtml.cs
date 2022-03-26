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
    public class DetailsModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public DetailsModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
