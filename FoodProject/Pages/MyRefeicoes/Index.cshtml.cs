using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyRefeicoes
{
    public class IndexModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public IndexModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Refeicao> Refeicao { get;set; }

        public async Task OnGetAsync()
        {
            Refeicao = await _context.Refeicaos
                //.Include(c => c.AlimentoRefeicao)
               //.ThenInclude(a => a.Gramas)
               .Include(c => c.AlimentoRefeicao)
               .ThenInclude(a => a.Alimentos)
               .Include(r => r.User)            
               .ToListAsync();



            //Refeicao = await _context.Refeicaos
            //    .Include(r => r.User).ToListAsync();
        }
    }
}
