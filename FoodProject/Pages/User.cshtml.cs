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
        

        public IEnumerable<Refeicao> results { get; set; }

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







            //foreach (var a in ali)
            //{
            //    foreach (var r in Refeicao)
            //    {
            //        if(r.AlimentoRefeicao. == )
            //    }

            //    //Refeicao.Where(r => r. .GetId() == a.id).First();

            //}








            //results = _context.Refeicaos.ToList();


            var mealExists = _context.Refeicaos
                .Where(u => u.User.Id == _userManager.GetUserId(User))
                .Any(x => x.Id >= 1);


            if (mealExists)
            {
                var x = _context.Refeicaos
                    .Where(u => u.User.Id == _userManager.GetUserId(User))
                    .OrderBy(x => x.HoraInicio)
                    .Last();


                
                var y = x.HoraFim;

                    
                if (x.HoraInicio <= x.HoraFim)
                {
                    var fasting = DateTime.Now - y;
                    fast.Add(fasting);
                }

                
            }
           
        }


        public void OnPost(DateTime? horaInicio, DateTime? horaFim)
        {

        results = (from x in _context.Refeicaos where(x.HoraInicio <= horaInicio) && (x.HoraFim >= horaFim) select x).ToList();


        }
    }
}
