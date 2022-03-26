using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Enteties;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyAlimentos
{
    public class EditModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public EditModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Alimento Alimento { get; set; }


        [BindProperty]
        public IList<SelectListItem> AcaoList { get; set; }




        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          



            Alimento = await _context.Alimentos
                .Include(a => a.Categoria)
                .Include(b => b.AlimentoAcaos)
                .FirstOrDefaultAsync(m => m.Id == id);


            AcaoList = _context.Acaos.ToList<Acao>().Select(m => new SelectListItem
            {
                Text = m.NomeAcao,
                Value = m.Id.ToString(),
                Selected = Alimento.AlimentoAcaos.Any(S => S.AcaoId == m.Id) ? true : false
            }).ToList<SelectListItem>();

            if (Alimento == null)
            {
                return NotFound();
            }
            ViewData["NomeCategoria"] = new SelectList(_context.Categorias, "Id", "NomeCategoria");
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


            Alimento AlimentoFromDB = await _context.Alimentos.
                Include(m => m.AlimentoAcaos).FirstOrDefaultAsync(m => m.Id == Alimento.Id);

            IList<AlimentoAcao> alimentoAcaos = new List<AlimentoAcao>();

            //variable to hold removed skills 
            IList<AlimentoAcao> AcaoToRemove = new List<AlimentoAcao>();

            //variable to hold newly added skills 
            IList<AlimentoAcao> AcaoToAdd = new List<AlimentoAcao>();

            foreach (SelectListItem acao in AcaoList)
            {
                if (acao.Selected)
                {
                    // Add all the selected skills to Employeeskills collection.
                    alimentoAcaos.Add(new AlimentoAcao
                    { AlimentoId = Alimento.Id, AcaoId = Convert.ToInt32(acao.Value) });


                    //if a new skill is assigned to the employee it is added
                    //to the SkillsToAdd collection
                    AlimentoAcao selectedAcao = AlimentoFromDB.AlimentoAcaos.
                        Where(m => m.AcaoId == Convert.ToInt32(acao.Value)).FirstOrDefault();
                    if (selectedAcao == null)
                    {
                        AcaoToAdd.Add(new AlimentoAcao
                        { AlimentoId = Alimento.Id, AcaoId = Convert.ToInt32(acao.Value) });

                    }
                }
            }
            //If a skill is not in the edited skill list, but present
            //in the skill list from the DB, it is added to 
            // the SkillsToRemove collection.
            foreach (AlimentoAcao alimentoacao in AlimentoFromDB.AlimentoAcaos)
            {
                if (alimentoAcaos.Any(e => e.AlimentoId == alimentoacao.AlimentoId
                && e.AcaoId == alimentoacao.AcaoId) == false)
                {
                    AcaoToRemove.Add(alimentoacao);
                }
            }

            //Section which assigns the modified values 
            //to the employee entity from the database
            AlimentoFromDB.Name = Alimento.Name;
            AlimentoFromDB.CategoriaId = Alimento.CategoriaId;

            //Delete the skills which are to be removed
            _context.RemoveRange(AcaoToRemove);


            //Adding newly assigned skills
            foreach (var aliAcao in AcaoToAdd)
            {

                AlimentoFromDB.AlimentoAcaos.Add(aliAcao);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentoExists(Alimento.Id))
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

            bool AlimentoExists(int id) => _context.Alimentos.Any(e => e.Id == id);













            //    if (!ModelState.IsValid)
            //    {
            //        return Page();
            //    }

            //    _context.Attach(Alimento).State = EntityState.Modified;

            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!AlimentoExists(Alimento.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //    return RedirectToPage("./Index");
            //}

        
    }
}
