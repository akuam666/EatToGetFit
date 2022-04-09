using FoodProject.Data.Ententies;
using FoodProject.Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Data.Enteties
{
    public class Alimento
    {

        public int Id { get; set; }


        [Required]
        [Display(Name = "Food")]
        public string Name { get; set; } 

        public int CategoriaId{ get; set; }

        [Display(Name = "Category")]
        public Categoria Categoria { get; set; }    

       
        public IList<AlimentoAcao> AlimentoAcaos { get; set; }

        [Display(Name = "Actions")]
        public ICollection<Acao> Acaos { get; set; }

        public ICollection<AlimentoRefeicao> AlimentoRefeicaos { get; set; }

       


    }
}
