using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CTrivia.API.DTO
{
    /// <summary>
    /// DTO de la respuesta recibida al consultar categorías en OpenTDB
    /// </summary>
    public class OpenTDBCategoriaResponseDTO
    {
        /// <summary>
        /// Lista de categorías de OpenTDB
        /// </summary>
        public List<OpenTDBCategoriaDTO> trivia_categories{ get; set; }
        /// <summary>
        /// Categoría de OpenTDB
        /// </summary>
        public class OpenTDBCategoriaDTO
        {
            /// <summary>
            /// Identificador de categoría
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// Nombre de categoría
            /// </summary>
            public string name { get; set; }
        }
    }
}
