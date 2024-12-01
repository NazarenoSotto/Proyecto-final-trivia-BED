using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Categoría de las preguntas
    /// </summary>
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
        /// Identificación de la categoría en su web externa
        /// </summary>
        public int WebId { get; set; }
        /// <summary>
        /// Web externa de la categoría
        /// </summary>
        public PaginasElegiblesEnum externalAPI { get; set; }

        /// <summary>
        /// Crear una nueva instancia de ECategoría
        /// </summary>
        /// <param name="pNombre">Nombre de la categoría</param>
        /// <param name="pWebId">Identificación externa de su respectiva web</param>
        /// <param name="externalApi">Enumerable que representa la web externa de la categoría</param>
        public ECategoria(string pNombre, int pWebId, PaginasElegiblesEnum externalApi)
        {
            NombreCategoria = pNombre;
            WebId = pWebId;
            externalAPI = externalApi;
        }

        /// <summary>
        /// Crear una nueva instancia de ECategoría con sólo el nombre
        /// </summary>
        /// <param name="pNombre">Nombre de la categoría</param>
        /// <param name="WebId">Identificación externa de su respectiva web</param>
        /// <param name="externalApi">Enumerable que representa la web externa de la categoría</param>
        public ECategoria(string pNombre)
        {
            NombreCategoria = pNombre;
            WebId = 0;
        }
        public ECategoria() { }
    }
}