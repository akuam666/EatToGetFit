using FoodProject.Data.Enteties;
using Microsoft.AspNetCore.Identity;

namespace FoodProject.Data.Ententies
{
    public class Favoritos
    {

        public int id { get; set; }

        public int AlimentoId { get; set; }

        public Alimento Alimentos { get; set; }


        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
