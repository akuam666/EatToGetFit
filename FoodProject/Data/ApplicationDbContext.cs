using FluentAssertions.Common;
using FoodProject.Data.Ententies;
using FoodProject.Data.Enteties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FoodProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<AlimentoAcao> AlimentoAcaos { get; set; }
        public DbSet<AlimentoRefeicao> AlimentoRefeicaos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Refeicao> Refeicaos { get; set; }

        public DbSet<Blacklist> Blacklist { get; set; }
        public DbSet<Acao> Acaos { get; set; }

        public DbSet<Favoritos> Favoritos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlimentoAcao>().HasKey(ES => new { ES.AlimentoId, ES.AcaoId });
           

            modelBuilder.Entity<Acao>().ToTable(nameof(Acao))
              .HasMany(c => c.Alimentos)
               .WithMany(i => i.Acaos);         

        }


        public void ImportFile()
        {
            string path = @"C:\Users\Bernardo\Downloads\123.csv";

            string[] lines = File.ReadAllLines(path);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',');

                if (Alimentos.Any(x => x.Name.Equals(columns[0])))
                {
                    continue;
                }

                Categoria category;
                Acao action;

                var categoryName = columns[1];

                if (!Categorias.Any(x => x.NomeCategoria.Equals(categoryName)))
                {
                    category = new Categoria() { NomeCategoria = categoryName };
                }
                else
                {
                    category = Categorias.FirstOrDefault(x => x.NomeCategoria.Equals(categoryName));
                }

                var food = new Alimento() { Name = columns[0], Categoria = category };

                Alimentos.Add(food);
                SaveChanges();

                var actionsNames = columns[2].Trim().Split('-');

                foreach (var actionName in actionsNames)
                {
                    if (!Acaos.Any(x => x.NomeAcao.Equals(actionName)))
                    {
                        action = new Acao() { NomeAcao = actionName };
                        Acaos.Add(action);
                        
                    }
                    else
                    {
                        action = Acaos.FirstOrDefault(x => x.NomeAcao.Equals(actionName));
                    }

                    var actionFood = new AlimentoAcao() { Acaos = action, Alimentos = food, AcaoId = action.Id, AlimentoId = food.Id };
                    AlimentoAcaos.Add(actionFood);
                }
                    SaveChanges();
            }
        }


           
      }




    }
