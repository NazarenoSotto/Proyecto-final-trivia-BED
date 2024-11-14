using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED
{
    public class Categoria
    {
        /// <summary>
        /// Identificador de la categor�a
        /// </summary>
        [Key]
        public int IdCategoria { get; set; }
        /// <summary>
        /// Nombre de la categor�a
        /// </summary>
        public string NombreCategoria { get; set; }
        /// <summary>
        /// Categor�a para las preguntas
        /// </summary>
        /// <param name="pNombre">Nombre de la categor�a</param>
        public int IdWeb { get; set; }
        public Categoria(string pNombre, int pIdWeb)
        {
            this.NombreCategoria = pNombre;
            this.IdWeb = pIdWeb;
        }

        public Categoria(string pNombre)
        {
            this.NombreCategoria = pNombre;
            this.IdWeb = 0;
        }
        public Categoria() { }
    }
}