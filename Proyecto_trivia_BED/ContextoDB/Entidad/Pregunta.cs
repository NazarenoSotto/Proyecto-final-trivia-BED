using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Pregunta
    /// </summary>
    public class Pregunta
    {
        /// <summary>
        /// Identificador de la pregunta
        /// </summary>
        [Key]
        public int IdPregunta { get; set; }
        /// <summary>
        /// Texto que define a la pregunta
        /// </summary>
        public string LaPregunta { get; set; }
        /// <summary>
        /// Categor�a a la que pertenece la pregunta
        /// </summary>
        public Categoria Categoria { get; set; }
        /// <summary>
        /// Dificultad de la pregunta
        /// </summary>
        public Dificultad Dificultad { get; set; }
        /// <summary>
        /// Respuestas incorrectas
        /// </summary>
        public IList<Respuesta> Respuestas { get; set; }

        /// <summary>
        /// Una pregunta
        /// </summary>
        /// <param name="pPregunta">Texto que define a la preg</param>
        /// <param name="pCategoria">Categor�a de la pregunta</param>
        /// <param name="pDif">Dificultad de la pregunta</param>
        public Pregunta(string pPregunta, Categoria pCategoria, Dificultad pDif, IList<Respuesta> pRespuestas)
        {
            LaPregunta = pPregunta;
            Categoria = pCategoria;
            Dificultad = pDif;
            Respuestas = pRespuestas;
        }

        public Pregunta() { }
    }
}