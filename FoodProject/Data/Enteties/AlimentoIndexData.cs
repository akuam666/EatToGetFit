using FoodProject.Data.Ententies;

namespace FoodProject.Data.Enteties
    
{
    public class AlimentoIndexData
    {
        public ICollection <Alimento> Alimentos{ get; set; }
        public ICollection <Acao> Acaos{ get; set; }
    }
}
