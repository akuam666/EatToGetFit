using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;

namespace FoodProject.Pages.MyCategorias
{
    public class IndexModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;

        public IndexModel(FoodProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Categoria> Categoria { get;set; }

        public async Task OnGetAsync()
        {
            Categoria = await _context.Categorias.ToListAsync();
        }
    }
}
