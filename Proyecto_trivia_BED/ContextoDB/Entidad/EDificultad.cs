using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    public class EDificultad
    {
        /// <summary>
        /// Identificador de la dificultad
        /// </summary>
        [Key]
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
        /// Dificultad de la pregunta
        /// </summary>
        public int webId { get; set; }
        /// <summary>
        /// Id de la dificultad en su respectiva web
        /// </summary>
        public PaginasElegiblesEnum externalAPI { get; set; }
        /// <summary>
        /// Web externa de la dificultad
        /// </summary>
        /// <param name="pNombre">Nombre de la dificultad</param>
        /// <param name="pValor">Valor de la dificultad en puntos</param>
        public EDificultad(string pNombre, int pValor)
        {
            NombreDificultad = pNombre;
            Valor = pValor;
        }

        public EDificultad() { }
    }
}