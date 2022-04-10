using FoodProject.Data.Ententies;
using FoodProject.Data.Enteties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodProject.Pages
{
    public class UserModel : PageModel
    {



        private readonly FoodProject.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
       

        public UserModel(FoodProject.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;


        }


     
        public List<TimeSpan> fast { get; set; } = new List<TimeSpan>();
 

        public IList<Refeicao> Refeicao { get; set; }
        public IList<Alimento> alimento { get; set; }
        



        public void OnGet()
        {

            Refeicao = _context.Refeicaos
              .Where(a => a.HoraInicio.DayOfYear == DateTime.Now.DayOfYear && a.User.Id == _userManager.GetUserId(User))
              .Include(c => c.AlimentoRefeicao)
              .ThenInclude(d => d.Alimentos)
              .ToList();


            // get action from meals
            var ali = _context.Alimentos.ToArray();

            int[] idRef = _context.Refeicaos
              .Where(a => a.HoraInicio.DayOfYear == DateTime.Now.DayOfYear && a.User.Id == _userManager.GetUserId(User))
              .Select(x => x.Id).ToArray();
            ViewData["idRef"] = idRef;

            int[] idAli = _context.AlimentoRefeicaos
                .Where(c => idRef.Contains(c.RefeicaoId))
                .Select(a => a.AlimentoId).ToArray();
            ViewData["idAli"] = idAli;

            int[] idAction = _context.AlimentoAcaos
               .Where(c => idAli.Contains(c.AlimentoId))
               .Select(a => a.AcaoId)
               .Distinct().ToArray();
            ViewData["idAction"] = idAction;

            string[] nameAction = _context.Acaos
                .Where(c => idAction.Contains(c.Id))
                .Select(a => a.NomeAcao).ToArray();
            ViewData["nameAction"] = nameAction;

            string[] missingAction = _context.Acaos
                .Where(c => !idAction.Contains(c.Id))
                .Select(a => a.NomeAcao).ToArray();
            ViewData["missingAction"] = missingAction;

            int[] missingActionId = _context.Acaos
                .Where(c => !idAction.Contains(c.Id))
                .Select(a => a.Id).ToArray();
            ViewData["missingActionId"] = missingActionId;

            int[] missingAliId = _context.AlimentoAcaos
                .Where(c => missingActionId.Contains(c.AcaoId))
                .Select(a => a.AlimentoId)
                .Distinct().ToArray();
            ViewData["missingAliId"] = missingAliId;

            //filter with blackList
            int[] excludeBlackListAli = _context.Blacklist
                .Where(a => !missingAliId.Contains(a.AlimentoId) && a.User.Id == _userManager.GetUserId(User))
                .Select(a => a.AlimentoId)
                .Take(6)
                .ToArray();
            ViewData["excludeBlackListAli"] = excludeBlackListAli;

            // filter by fav
            int[] getFavouriteListAli = _context.Favoritos
               .Where(a => excludeBlackListAli.Contains(a.AlimentoId) && a.User.Id == _userManager.GetUserId(User))
               .Select(a => a.AlimentoId)
               .ToArray();
            ViewData["getFavouriteListAli"] = getFavouriteListAli;

            //get sugestions names of alis 
            string[] getSugestionAli = _context.Alimentos
                .Where(a => missingAliId.Contains(a.Id))
                .Select(a => a.Name)
                .Take(6)
                .ToArray();
            ViewData["getSugestionAli"] = getSugestionAli;

            //get sugestions names of alis  by id
            int[] getSugestionAliId = _context.Alimentos
                .Where(a => getFavouriteListAli.Contains(a.Id))
                .Select(a => a.Id)
                .ToArray();
            ViewData["getSugestionAliId"] = getSugestionAliId;

            string[] getSugestionAliWithoutFav = _context.Alimentos
               .Where(a => getFavouriteListAli.Contains(a.Id) && !getSugestionAliId.Contains(a.Id))
               .Select(a => a.Name)
               .ToArray();
            ViewData["getSugestionAliWithoutFav"] = getSugestionAliWithoutFav;





            var mealExists = _context.Refeicaos
                .Where(a => a.User.Id == _userManager.GetUserId(User))
                .Any(b => b.Id >= 1);


            if (mealExists)
            {
                var x = _context.Refeicaos
                    .Where(a => a.User.Id == _userManager.GetUserId(User))
                    .OrderBy(b => b.HoraInicio)
                    .Last();


                
                var y = x.HoraFim;

                    
                if (x.HoraInicio <= x.HoraFim)
                {
                    var fasting = DateTime.Now - y;
                    fast.Add(fasting);
                }

                
            }
        }
    }
}
