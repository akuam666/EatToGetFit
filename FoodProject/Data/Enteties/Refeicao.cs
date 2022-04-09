using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Data.Ententies
{
    public class Refeicao
    {

        public int Id { get; set; }

      
        public ICollection<AlimentoRefeicao> AlimentoRefeicao { get; set; }

        [Required]
        [Display(Name = "Meal Name")]
        public string NomeRefeição { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public DateTime HoraInicio { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Time")]
        public DateTime HoraFim { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }


      


    }
}
