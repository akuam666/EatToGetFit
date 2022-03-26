
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using FoodProject.Data.Enteties;
using System.Diagnostics;

namespace FoodProject.Pages.MyAlimentos
{
    public class IndexModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public IndexModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        //public IList<Alimento> Alimento { get;set; }
        public  IList <Alimento> Alimento { get; set; }

        //public AlimentoIndexData AlimentoData { get;set; }
        //public AlimentoAcao Alimento1 { get; set; }
        //public int AlimentoID { get; set; }
        //public int AcaoID { get; set; }



        public async Task OnGetAsync()
        {
            

            
           
            //AlimentoData= new AlimentoIndexData();
            Alimento = await _context.Alimentos
                .Include(c=>c.Categoria)
                .Include(a => a.AlimentoAcaos)
                .ThenInclude(b => b.Acaos) // tabela intermedia
                .ToListAsync();
            //.Include(a => a.Acaos)
            //  .ThenInclude(b =>b.NomeAcao)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //var b = 10;
            

            //Alimento = await _context.Alimentos.Include(m => m.Categoria).ThenInclude(s => s.Acaos).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);


            // if (id != null)
            //{
            //     AlimentoID = id.Value;
            //    Alimento alimento= AlimentoData.Alimentos
            //      .Where(i => i.Id == id.Value).Single();
            //   AlimentoData.Acaos = alimento.Acaos;
            // }

            // if (acaoID != null)
            // {
            //     AcaoID = acaoID.Value;
            //    var selectedCourse = AlimentoData.Acaos
            //         .Where(x => x.Id == acaoID).Single();
            //await _context.Entry(selectedCourse)
            //            .Collection(x => x.).LoadAsync();
            //foreach (Enrollment enrollment in selectedCourse.Enrollments)
            //{
            //   await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
            }
            //InstructorData.Enrollments = selectedCourse.Enrollments;
        }




}



