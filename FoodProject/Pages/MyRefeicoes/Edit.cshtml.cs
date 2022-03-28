using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;
using FoodProject.Data.Enteties;

namespace FoodProject.Pages.MyRefeicoes
{
    public class EditModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public EditModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Refeicao Refeicao { get; set; }

        [BindProperty]
        public Alimento Alimento { get; set; }  



       [BindProperty]
        public IList<SelectListItem> AlimentoList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Refeicao = await _context.Refeicaos
                //.Include(a => a.Categoria)
                .Include(b => b.AlimentoRefeicao)
                .ThenInclude(c => c.Alimentos)
                .FirstOrDefaultAsync(m => m.Id == id);


            AlimentoList = _context.Alimentos.ToList<Alimento>().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString(),
                Selected = Refeicao.AlimentoRefeicao.Any(S => S.AlimentoId == m.Id) ? true : false
            }).ToList<SelectListItem>();

       

            if (Refeicao == null)
            {
                return NotFound();
            }
           
           return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            Refeicao RefeicaoFromDB = await _context.Refeicaos.
                Include(m => m.AlimentoRefeicao).FirstOrDefaultAsync(m => m.Id == Refeicao.Id);

            IList<AlimentoRefeicao> alimentoRefeicao = new List<AlimentoRefeicao>();

            //variable to hold removed skills 
            IList<AlimentoRefeicao> AlimentoToRemove = new List<AlimentoRefeicao>();

            //variable to hold newly added skills 
            IList<AlimentoRefeicao> AlimentoToAdd = new List<AlimentoRefeicao>();

            foreach (SelectListItem ali in AlimentoList)
            {
                if (ali.Selected)
                {
                    // Add all the selected skills to Employeeskills collection.
                    alimentoRefeicao.Add(new AlimentoRefeicao
                    { RefeicaoId = Refeicao.Id, AlimentoId = Convert.ToInt32(ali.Value) });


                    //if a new skill is assigned to the employee it is added
                    //to the SkillsToAdd collection
                    AlimentoRefeicao selectedAlimento = RefeicaoFromDB.AlimentoRefeicao.
                        Where(m => m.AlimentoId == Convert.ToInt32(ali.Value)).FirstOrDefault();
                    if (selectedAlimento == null)
                    {
                        AlimentoToAdd.Add(new AlimentoRefeicao
                        { RefeicaoId = Alimento.Id, AlimentoId = Convert.ToInt32(ali.Value) });

                    }
                }
            }
            //If a skill is not in the edited skill list, but present
            //in the skill list from the DB, it is added to 
            // the SkillsToRemove collection.
            foreach (AlimentoRefeicao alimentorefeicao in RefeicaoFromDB.AlimentoRefeicao)
            {
                if (alimentoRefeicao.Any(e => e.RefeicaoId == alimentorefeicao.RefeicaoId
                && e.AlimentoId == alimentorefeicao.AlimentoId) == false)
                {
                    AlimentoToRemove.Add(alimentorefeicao);
                }
            }

            //Section which assigns the modified values 
            //to the employee entity from the database
            RefeicaoFromDB.NomeRefeição = Refeicao.NomeRefeição;
            //RefeicaoFromDB.CategoriaId = Alimento.CategoriaId;

            //Delete the skills which are to be removed
            _context.RemoveRange(AlimentoToRemove);


            //Adding newly assigned skills
            foreach (var aliAcao in AlimentoToAdd)
            {

                RefeicaoFromDB.AlimentoRefeicao.Add(aliAcao);
            }



            //_context.Attach(Refeicao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefeicaoExists(Refeicao.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RefeicaoExists(int id)
        {
            return _context.Refeicaos.Any(e => e.Id == id);
        }
    }
}
