using System.Collections.Generic;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO
{
    public class CalculoPuntajeDTO
    {
        public UsuarioDTO Usuario { get; set; }
        public List<PreguntaDTO> PreguntasEvaluadas { get; set; }
        public int Tiempo { get; set; }

    }
}
