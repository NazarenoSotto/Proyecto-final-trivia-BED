using Microsoft.EntityFrameworkCore;

namespace Proyecto_trivia_BED
{
    public class TriviaContext : DbContext  // Asegúrate de que herede de DbContext
    {
        public TriviaContext(DbContextOptions<TriviaContext> options) : base(options) { }

        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Dificultad> Dificultades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Puntaje> Puntajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ejemplo de seed data para la entidad `Pregunta`
            modelBuilder.Entity<Dificultad>().HasData(
                new Dificultad { IdDificultad = 1, NombreDificultad = "easy", Valor = 1 },
                new Dificultad { IdDificultad = 2, NombreDificultad = "medium", Valor = 3 },
                new Dificultad { IdDificultad = 3, NombreDificultad = "hard", Valor = 5 }
            );

        }
    }

}