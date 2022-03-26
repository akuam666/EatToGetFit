using FoodProject.Data.Enteties;

namespace FoodProject.Data.Ententies
{
    public class Acao
    {

        public int Id { get; set; }

        public string NomeAcao { get; set; }

        //public int AlimentoAcaoId { get; set; }
        public IList<AlimentoAcao> AlimentoAcaos { get; set; }

        public ICollection<Alimento> Alimentos { get; set; }

    }
}
