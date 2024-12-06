using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.Controladores.CTrivia.Modelo.DTO
{
    /// <summary>
    /// DTO de Categoría
    /// </summary>
    public class CategoriaDTO
    {
        /// <summary>
        /// Identificador de la categoría
        /// </summary>
        public int IdCategoria{ get; set; }
        /// <summary>
        /// Nombre de la categoría
        /// </summary>
        public string NombreCategoria { get; set; }
        /// <summary>
        /// Id externa de la categoría
        /// </summary>
        public int WebId { get; set; }

    }
}
