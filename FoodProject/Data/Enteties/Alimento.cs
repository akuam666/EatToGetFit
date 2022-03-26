using FoodProject.Data.Ententies;
using FoodProject.Data.Enteties;

namespace FoodProject.Data.Enteties
{
    public class Alimento
    {

        public int Id { get; set; }
        public string Name { get; set; } 

        public int CategoriaId{ get; set; }
        public Categoria Categoria { get; set; }    

        //public int AlimentoAcaoId { get; set; }
        public IList<AlimentoAcao> AlimentoAcaos { get; set; }

        public ICollection<Acao> Acaos { get; set; }

        public ICollection<AlimentoRefeicao> AlimentoRefeicaos { get; set; }

        //public int AlimentoRefeicaoId { get; set; }
        //public ICollection<Refeicao> Refeicao { get; set; }


    }
}
