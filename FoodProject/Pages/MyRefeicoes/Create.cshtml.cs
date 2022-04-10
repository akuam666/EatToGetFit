using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodProject.Data;
using FoodProject.Data.Ententies;
using System.ComponentModel.DataAnnotations;
using FoodProject.Data.Enteties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodProject.Pages.MyRefeicoes
{

    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(FoodProject.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [BindProperty]
        public IList<SelectListItem> AlimentoList { get; set; }


        public List<SelectListItem> Alimentos{ get; set; }




        public IActionResult OnGet()
        {

            AlimentoList = _context.Alimentos           
               .ToList<Alimento>()
               .Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() })
               .ToList<SelectListItem>();

            return Page();          
        }


        [BindProperty]
        public Refeicao Refeicao { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var alimentos = (from alimento in _context.Alimentos
                             select new SelectListItem
                             {
                                 Text = alimento.Name,
                                 Value = alimento.Id.ToString()
                             }).ToList();
            Alimentos = alimentos;

       




            var userId = _userManager.GetUserId(User);
            Refeicao.UserId = userId;


            IList<AlimentoRefeicao> AlimentoRefeicaos = new List<AlimentoRefeicao>();

            foreach (SelectListItem acao in AlimentoList)
            {
                if (acao.Selected)
                {
                    AlimentoRefeicaos.Add(new AlimentoRefeicao { AlimentoId = Convert.ToInt32(acao.Value) });
                }
            }
           
            Refeicao.AlimentoRefeicao = AlimentoRefeicaos;
            _context.Refeicaos.Add(Refeicao);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");





        }
    }
}
