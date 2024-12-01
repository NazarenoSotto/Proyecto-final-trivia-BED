using System.Collections.Generic;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO
{
    /// <summary>
    /// DTO para la respuesta del cálculo de puntaje
    /// </summary>
    public class CalculoPuntajeDTO
    {
        /// <summary>
        /// Usuario de la trivia
        /// </summary>
        public UsuarioDTO Usuario { get; set; }
        /// <summary>
        /// Lista de preguntas evaluadas
        /// </summary>
        public List<PreguntaDTO> PreguntasEvaluadas { get; set; }
        /// <summary>
        /// Tiempo de resolución de la trivia
        /// </summary>
        public int Tiempo { get; set; }

    }
}
