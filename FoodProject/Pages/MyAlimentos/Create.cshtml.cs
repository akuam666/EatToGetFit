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
        [BindProperty]
        [MaxLength(50)]
        [Display(Name = "Add a New Acao")]
        public String NewAcao { get; set; }

       


        public IActionResult OnGet()
        {
            AcaoList = _context.Acaos.ToList<Acao>()
               .Select(m => new SelectListItem { Text = m.NomeAcao, Value = m.Id.ToString() })
               .ToList<SelectListItem>();
            //return Page();

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NomeCategoria");
       
            return Page();
        }

        [BindProperty]
        public Alimento Alimento { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
            //checking if a new skills was added or not
            if (!string.IsNullOrEmpty(NewAcao))
            {
                //when a new skill is added, create a new skill instance and assign it to and EmployeeSkill entity. 
                //It is then assigned to a collection of Employeeskills
                Acao acao = new Acao { NomeAcao = NewAcao };
                AlimentoAcao alimentoAcao = new AlimentoAcao { Acaos = (acao )};
                AlimentoAcaos.Add(alimentoAcao);
            }
            //The collection of Employeeskills is assigned to the Employee entity and saved to the database
            Alimento.AlimentoAcaos = AlimentoAcaos;
            _context.Alimentos.Add(Alimento);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");





          
        }
    }
}
