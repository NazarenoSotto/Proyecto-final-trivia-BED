using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Respuesta de una pregunta
    /// </summary>
    public class Respuesta
    {
        /// <summary>
        /// Identificador de la respuesta
        /// </summary>
        [Key]
        public int IdRespuesta { get; set; }

        /// <summary>
        /// Define el texto de la respuesta
        /// </summary>
        public string SRespuesta { get; set; }

        /// <summary>
        /// Define si la respuesta es correcta o no
        /// </summary>
        public bool Correcta { get; set; }
        /// <summary>
        /// Instanciar una respuesta
        /// </summary>
        /// <param name="pRespuesta">Texto que define la respuesta</param>
        /// <param name="pCorrecta">Define si es correcta(true) o incorrecta(false)</param>
        public Respuesta(string pRespuesta, bool pCorrecta)
        {
            SRespuesta = pRespuesta;
            Correcta = pCorrecta;
        }

        public Respuesta() { }

    }
}