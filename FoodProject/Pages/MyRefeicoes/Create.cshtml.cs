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
        [BindProperty]
        [MaxLength(50)]
        [Display(Name = "Add a New Alimento")]
        public String NewAlimento { get; set; }

        //public Refeicao Refeicao { get; set; }


        public IActionResult OnGet()
        {

            //var userId = _userManager.GetUserId(User);
            //ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "Email");
        


            //var refeicao = new Refeicao()
            //{

            //    UserId = userId

            //};
            //Refeicao = _context.Refeicaos
            //   .Include(t => t.User);


            AlimentoList = _context.Alimentos.ToList<Alimento>()
               .Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() })
               .ToList<SelectListItem>();

            return Page();


           
        }

        [BindProperty]
        public Refeicao Refeicao { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


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
            //checking if a new skills was added or not
            if (!string.IsNullOrEmpty(NewAlimento))
            {
                //when a new skill is added, create a new skill instance and assign it to and EmployeeSkill entity. 
                //It is then assigned to a collection of Employeeskills
                Alimento alimento = new Alimento { Name = NewAlimento };
                AlimentoRefeicao alimentoRefeicao = new AlimentoRefeicao { Alimentos = (alimento) };
                AlimentoRefeicaos.Add(alimentoRefeicao);
            }
            //The collection of Employeeskills is assigned to the Employee entity and saved to the database
            Refeicao.AlimentoRefeicao = AlimentoRefeicaos;
            _context.Refeicaos.Add(Refeicao);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");





            //_context.Refeicaos.Add(Refeicao);
            //await _context.SaveChangesAsync();

            //return RedirectToPage("./Index");
        }
    }
}
