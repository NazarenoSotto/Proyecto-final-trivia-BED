using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Dificultad de las preguntas
    /// </summary>
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
        /// Identificación en la web externa
        /// </summary>
        public int webId { get; set; }
        /// <summary>
        /// Web externa de la dificultad
        /// </summary>
        public PaginasElegiblesEnum externalAPI { get; set; }
        /// <summary>
        /// Crear una nueva instancia de EDificultad
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