
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

        public  IList <Alimento> Alimento { get; set; }

        
        

        public async Task OnGetAsync()
        {
           
        
            Alimento = await _context.Alimentos
                .Include(c=>c.Categoria)
                .Include(a => a.AlimentoAcaos)
                .ThenInclude(b => b.Acaos) 
                .ToListAsync();



        }
        
        }




}



