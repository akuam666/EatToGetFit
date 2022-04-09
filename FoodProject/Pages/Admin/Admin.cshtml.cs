using FoodProject.Data;
using FoodProject.Data.Ententies;
using FoodProject.Data.Enteties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FoodProject.Pages.Admin
{
    public class AdminModel : PageModel
    {
        private readonly FoodProject.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
       

        public AdminModel(FoodProject.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            

        }

        public IList<AlimentoRefeicao> MaisConsumidos { get; set; }

        public IList<Alimento> Alimento { get; set; }

      



       
      

        public void OnGet()
        {



            var abc = _context.AlimentoRefeicaos
                .Include(a => a.Alimentos)
                .GroupBy(product => product.AlimentoId)
                .OrderByDescending(grp => grp.Count())
                .Take(10)
                .Select(grp => grp.Key)
                .ToList();

            var def = _context.Refeicaos
                .Include(b => b.User)
                .GroupBy(a => a.UserId)
                .OrderByDescending(grp => grp.Count())
                .Take(10)
                .Select(grp => grp.Key)
                .ToList();
        

            ViewData["Max"] = _context.Refeicaos
                .Count();


            ViewData["table"] = _userManager.Users
                .Count();
            
            ViewData["students"] = _context.Alimentos
                .Where(u => abc.Contains(u.Id));

            ViewData["def"] = _userManager.Users
                .Where(u => def.Contains(u.Id));


            var ghi = _context.Refeicaos       
               .ToList();


           

            

        }
        public void OnPost()
        {



            _context.ImportFile();
          



            var abc = _context.AlimentoRefeicaos
               .Include(a => a.Alimentos)
               .GroupBy(product => product.AlimentoId)
               .OrderByDescending(grp => grp.Count())
               .Take(10)
               .Select(grp => grp.Key)
               .ToList();

            var def = _context.Refeicaos
                .Include(b => b.User)
                .GroupBy(a => a.UserId)
                .OrderByDescending(grp => grp.Count())
                .Take(10)
                .Select(grp => grp.Key)
                .ToList();


            ViewData["Max"] = _context.Refeicaos
                .OrderByDescending(p => p.Id)
                .FirstOrDefault().Id;

            ViewData["table"] = _userManager.Users
                .Count();

            ViewData["students"] = _context.Alimentos
                .Where(u => abc.Contains(u.Id));

            ViewData["def"] = _userManager.Users
                .Where(u => def.Contains(u.Id));


            var ghi = _context.Refeicaos             
               .ToList();

        }
    }
}
