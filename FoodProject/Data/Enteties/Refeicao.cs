using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Data.Ententies
{
    public class Refeicao
    {

        public int Id { get; set; }

        //public int AlimentoRefeicaoId { get; set; }
        public ICollection<AlimentoRefeicao> AlimentoRefeicao { get; set; }

        public string NomeRefeição { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Refeição")]
        public DateTime RefeicaoData { get; set; }





        public string UserId { get; set; }
        public IdentityUser User { get; set; }


    }
}
