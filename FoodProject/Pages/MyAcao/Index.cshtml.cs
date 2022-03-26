using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyAcao
{
    public class IndexModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public IndexModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Acao> Acao { get;set; }

        public async Task OnGetAsync()
        {
            Acao = await _context.Acaos.ToListAsync();
        }
    }
}
