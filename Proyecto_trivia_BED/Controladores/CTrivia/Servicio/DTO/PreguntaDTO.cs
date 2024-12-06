using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CTrivia.Modelo.DTO
{
    /// <summary>
    /// DTO de Pregunta
    /// </summary>
    public class PreguntaDTO
    {
        public int IdPregunta { get; set; }
        /// <summary>
        /// Texto que define a la pregunta
        /// </summary>
        public string LaPregunta { get; set; }
        /// <summary>
        /// Categoría a la que pertenece la pregunta
        /// </summary>
        public CategoriaDTO Categoria { get; set; }
        /// <summary>
        /// Dificultad de la pregunta
        /// </summary>
        public DificultadDTO Dificultad { get; set; }
        /// <summary>
        /// Respuestas incorrectas
        /// </summary>
        public IList<RespuestaDTO> Respuestas { get; set; }

    }
}