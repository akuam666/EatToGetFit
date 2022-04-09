using FoodProject.Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Data.Ententies
{
    public class Acao
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Action")]
        public string NomeAcao { get; set; }

       
        public IList<AlimentoAcao> AlimentoAcaos { get; set; }

        public ICollection<Alimento> Alimentos { get; set; }

    }
}
