using FoodProject.Data.Ententies;
using FoodProject.Data.Enteties;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FoodProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<AlimentoAcao> AlimentoAcaos { get; set; }
        public DbSet<AlimentoRefeicao> AlimentoRefeicaos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Refeicao> Refeicaos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodProject.Data.Ententies.Acao> Acaos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlimentoAcao>().HasKey(ES => new { ES.AlimentoId, ES.AcaoId });
            //modelBuilder.Entity<AlimentoRefeicao>().HasKey(ES => new { ES.AlimentoId, ES.RefeicaoId });

            modelBuilder.Entity<Acao>().ToTable(nameof(Acao))
              .HasMany(c => c.Alimentos)
               .WithMany(i => i.Acaos);



            modelBuilder.Entity<Acao>().HasData(
            new Acao { Id = 1, NomeAcao = "Ossos" },
            new Acao { Id = 2, NomeAcao = "Coraçao" },
            new Acao { Id = 3, NomeAcao = "Flexibility" },
            new Acao { Id = 4, NomeAcao = "Innovation" }

            );




            //protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{
            //    modelBuilder.Entity<Alimento>().ToTable(nameof(Alimento))
            //      .HasMany(c => c.Acao)
            //        .WithMany(i => i.Alimento);


            //}

        }


        public DbSet<FoodProject.Data.Ententies.Favoritos> Favoritos { get; set; }


    }
}