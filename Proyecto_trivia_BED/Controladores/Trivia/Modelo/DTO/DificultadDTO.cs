using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{
    /// <summary>
    /// DTO de dificultad
    /// </summary>
    public class DificultadDTO
    {
        /// <summary>
        /// Identificador de la dificultad
        /// </summary>
        public int IdDificultad { get; set; }
        /// <summary>
        /// Nombre de la dificultad
        /// </summary>
        public string NombreDificultad { get; set; }
        /// <summary>
        /// Valor de la dificultad en puntos
        /// </summary>
        public int Valor { get; set; }
        /// <summary>
        /// Identificación en la web externa
        /// </summary>
        public int webId { get; set; }

    }
}