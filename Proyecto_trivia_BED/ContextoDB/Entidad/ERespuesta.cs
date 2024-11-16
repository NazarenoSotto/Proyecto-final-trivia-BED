using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    public class ERespuesta
    {
        /// <summary>
        /// Identificador de la respuesta
        /// </summary>
        [Key]
        public int IdRespuesta { get; set; }

        /// <summary>
        /// String que define la respuesta
        /// </summary>
        public string SRespuesta { get; set; }

        /// <summary>
        /// Booleano que define si la respuesta es correcta o no
        /// </summary>
        public bool Correcta { get; set; }
        /// <summary>
        /// Posible respuesta a una pregunta
        /// </summary>
        /// <param name="pRespuesta">Texto que define la respuesta</param>
        /// <param name="pCorrecta">Define si es correcta(true) o incorrecta(false)</param>
        public ERespuesta(string pRespuesta, bool pCorrecta)
        {
            SRespuesta = pRespuesta;
            Correcta = pCorrecta;
        }

        public ERespuesta() { }

    }
}