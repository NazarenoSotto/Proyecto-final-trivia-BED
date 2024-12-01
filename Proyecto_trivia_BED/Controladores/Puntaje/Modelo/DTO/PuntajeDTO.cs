using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;
using System;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO
{
    /// <summary>
    /// DTO de Puntaje
    /// </summary>
    public class PuntajeDTO
    {
        /// <summary>
        /// Identificador del puntaje
        /// </summary>
        public int IdPuntaje { get; set; }
        /// <summary>
        /// Usuario relacionado al puntaje
        /// </summary>
        public UsuarioDTO Usuario { get; set; }
        /// <summary>
        /// Valor del puntaje
        /// </summary>
        public float ValorPuntaje { get; set; }
        /// <summary>
        /// Fecha de obtención del puntaje
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Tiempo de resolución de la trivia
        /// </summary>
        public int Tiempo { get; set; }
        /// <summary>
        /// Cantidad de preguntas respondidas
        /// </summary>
        public int CantidadPreguntas { get; set; }
        /// <summary>
        /// Cantidad de preguntas respondidas correctamente
        /// </summary>
        public int CantidadCorrectas { get; set; }

    }
}
