using FoodProject.Data.Enteties;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Data.Ententies
{
    public class Blacklist
    {


        public int id { get; set; }

        public int AlimentoId { get; set; }

        public Alimento Alimentos { get; set; }


        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Added at")]
        public DateTime DiaBlacklist { get; set; }

    }
}
