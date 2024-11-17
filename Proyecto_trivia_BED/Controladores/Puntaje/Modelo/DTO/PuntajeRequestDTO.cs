using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO
{
    public class PuntajeRequestDTO
    {
        public UsuarioDTO Usuario { get; set; }
        public int CantCorrectas { get; set; }
        public int CantPreguntas { get; set; }
        public int Tiempo { get; set; }
        public DificultadDTO Dificultad { get; set; }

    }
}
