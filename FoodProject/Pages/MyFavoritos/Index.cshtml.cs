using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodProject.Data;
using FoodProject.Data.Ententies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FoodProject.Pages.MyFavoritos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(FoodProject.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Favoritos> Favoritos { get;set; }

        public async Task OnGetAsync()
        {
            Favoritos = await _context.Favoritos
                 .Where(u => u.User.Id == _userManager.GetUserId(User)).Include(c => c.User)
                .Include(f => f.Alimentos)
                .Include(f => f.User).ToListAsync();
        }
    }
}
