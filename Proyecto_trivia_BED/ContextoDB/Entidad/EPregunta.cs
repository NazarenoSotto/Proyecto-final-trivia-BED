using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Pregunta
    /// </summary>
    public class EPregunta
    {
        //private int iIdPregunta;
        //private string iLaPregunta;
        //private string iReferencias;
        //private IList<string> iRespInc;
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
        /// Categoría a la que pertenece la pregunta
        /// </summary>
        public ECategoria Categoria { get; set; }
        /// <summary>
        /// Dificultad de la pregunta
        /// </summary>
        public EDificultad Dificultad { get; set; }
        /// <summary>
        /// Respuestas incorrectas
        /// </summary>
        public IList<ERespuesta> Respuestas { get; set; }

        /// <summary>
        /// Una pregunta
        /// </summary>
        /// <param name="pPregunta">Texto que define a la preg</param>
        /// <param name="pCategoria">Categoría de la pregunta</param>
        /// <param name="pDif">Dificultad de la pregunta</param>
        /// <param name="pRespIncorrectas">Lista de respuestas incorrectas</param>
        /// <param name="pRespCorrecta">La respuesta correcta</param>
        public EPregunta(string pPregunta, ECategoria pCategoria, EDificultad pDif, IList<ERespuesta> pRespuestas)
        {
            LaPregunta = pPregunta;
            Categoria = pCategoria;
            Dificultad = pDif;
            Respuestas = pRespuestas;
        }

        public EPregunta() { }
    }
}