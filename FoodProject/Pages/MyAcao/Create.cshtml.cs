using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyAcao
{
    public class CreateModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public CreateModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Acao Acao { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Acaos.Add(Acao);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
