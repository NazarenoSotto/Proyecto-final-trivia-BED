using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    public class ECategoria
    {
        /// <summary>
        /// Identificador de la categoría
        /// </summary>
        [Key]
        public int IdCategoria { get; set; }
        /// <summary>
        /// Nombre de la categoría
        /// </summary>
        public string NombreCategoria { get; set; }
        /// <summary>
        /// Categoría para las preguntas
        /// </summary>
        /// <param name="pNombre">Nombre de la categoría</param>
        public int IdWeb { get; set; }
        /// <summary>
        /// Id de la categoria en su respectiva web
        /// </summary>
        public PaginasElegiblesEnum externalAPI { get; set; }
        /// <summary>
        /// Web externa de la categoria
        /// </summary>


        public ECategoria(string pNombre, int pIdWeb)
        {
            NombreCategoria = pNombre;
            IdWeb = pIdWeb;
        }

        public ECategoria(string pNombre)
        {
            NombreCategoria = pNombre;
            IdWeb = 0;
        }
        public ECategoria() { }
    }
}