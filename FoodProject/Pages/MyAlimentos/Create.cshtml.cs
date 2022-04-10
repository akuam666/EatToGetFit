using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodProject.Data;
using FoodProject.Data.Enteties;
using FoodProject.Data.Ententies;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Pages.MyAlimentos
{
    public class CreateModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public CreateModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<SelectListItem> AcaoList { get; set; }
     

       


        public IActionResult OnGet()
        {
            AcaoList = _context.Acaos.ToList<Acao>()
               .Select(m => new SelectListItem { Text = m.NomeAcao, Value = m.Id.ToString() })
               .ToList<SelectListItem>();
            

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NomeCategoria");
       
            return Page();
        }

        [BindProperty]
        public Alimento Alimento { get; set; }

     
        public async Task<IActionResult> OnPostAsync()
        {


            if (!ModelState.IsValid)
            {
                return Page();
            }
            IList<AlimentoAcao> AlimentoAcaos = new List<AlimentoAcao>();

            foreach (SelectListItem acao in AcaoList)
            {
                if (acao.Selected)
                {
                    AlimentoAcaos.Add(new AlimentoAcao { AcaoId = Convert.ToInt32(acao.Value) });
                }
            }
       
          
            Alimento.AlimentoAcaos = AlimentoAcaos;
            _context.Alimentos.Add(Alimento);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

          
        }
    }
}
