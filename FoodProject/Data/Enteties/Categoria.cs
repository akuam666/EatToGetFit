using System.ComponentModel.DataAnnotations;

namespace FoodProject.Data.Ententies
{
    public class Categoria
    {

        public int Id { get; set; }


        [Required]
        [Display(Name = "Category")]
        public string NomeCategoria { get; set; }
    }
}
