using FoodProject.Data.Enteties;

namespace FoodProject.Data.Ententies
{
    public class AlimentoRefeicao
    {
        public int Id { get; set; }
        public int AlimentoId { get; set; }
        public Alimento Alimentos { get; set; }

        public int RefeicaoId { get; set; }
        public Refeicao Refeicaos { get; set; }
        public int Gramas { get; set; }




    }
}
