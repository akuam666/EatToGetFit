﻿using System;
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

namespace FoodProject.Pages.MyRefeicoes
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

        public IList<Refeicao> Refeicao { get;set; }

        public async Task OnGetAsync()
        {
            Refeicao = await _context.Refeicaos.
                Where(u => u.User.Id == _userManager.GetUserId(User)).Include(c => c.User)
               //.Include(c => c.AlimentoRefeicao)
               //.ThenInclude(a => a.Gramas)
               .Include(c => c.AlimentoRefeicao)
               .ThenInclude(a => a.Alimentos)
               //.Include(r => r.User)


              
               .ToListAsync();



            //Refeicao = await _context.Refeicaos
            //    .Include(r => r.User).ToListAsync();
        }
    }
}
