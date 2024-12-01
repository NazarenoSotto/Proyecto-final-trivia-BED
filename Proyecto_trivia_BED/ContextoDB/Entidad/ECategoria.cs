using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Categor�a de las preguntas
    /// </summary>
    public class ECategoria
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
        /// Identificaci�n de la categor�a en su web externa
        /// </summary>
        public int WebId { get; set; }
        /// <summary>
        /// Web externa de la categor�a
        /// </summary>
        public PaginasElegiblesEnum externalAPI { get; set; }

        /// <summary>
        /// Crear una nueva instancia de ECategor�a
        /// </summary>
        /// <param name="pNombre">Nombre de la categor�a</param>
        /// <param name="pWebId">Identificaci�n externa de su respectiva web</param>
        /// <param name="externalApi">Enumerable que representa la web externa de la categor�a</param>
        public ECategoria(string pNombre, int pWebId, PaginasElegiblesEnum externalApi)
        {
            NombreCategoria = pNombre;
            WebId = pWebId;
            externalAPI = externalApi;
        }

        /// <summary>
        /// Crear una nueva instancia de ECategor�a con s�lo el nombre
        /// </summary>
        /// <param name="pNombre">Nombre de la categor�a</param>
        /// <param name="WebId">Identificaci�n externa de su respectiva web</param>
        /// <param name="externalApi">Enumerable que representa la web externa de la categor�a</param>
        public ECategoria(string pNombre)
        {
            NombreCategoria = pNombre;
            WebId = 0;
        }
        public ECategoria() { }
    }
}