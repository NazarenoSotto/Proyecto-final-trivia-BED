using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB.Entidad;

namespace Proyecto_trivia_BED.ContextoDB
{
    public class TriviaContext : DbContext  // Asegúrate de que herede de DbContext
    {
        public TriviaContext(DbContextOptions<TriviaContext> options) : base(options) { }

        public DbSet<EPregunta> Preguntas { get; set; }
        public DbSet<ERespuesta> Respuestas { get; set; }
        public DbSet<ECategoria> Categorias { get; set; }
        public DbSet<EDificultad> Dificultades { get; set; }
        public DbSet<EUsuario> Usuarios { get; set; }
        public DbSet<EPuntaje> Puntajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ejemplo de seed data para la entidad `Pregunta`
            modelBuilder.Entity<EDificultad>().HasData(
                new EDificultad { IdDificultad = 1, NombreDificultad = "easy", Valor = 1, webId = 1, externalAPI = PaginasElegiblesEnum.OpenTDB },
                new EDificultad { IdDificultad = 2, NombreDificultad = "medium", Valor = 3, webId = 3, externalAPI = PaginasElegiblesEnum.OpenTDB },
                new EDificultad { IdDificultad = 3, NombreDificultad = "hard", Valor = 5, webId = 5, externalAPI = PaginasElegiblesEnum.OpenTDB }
            );

        }
    }

}