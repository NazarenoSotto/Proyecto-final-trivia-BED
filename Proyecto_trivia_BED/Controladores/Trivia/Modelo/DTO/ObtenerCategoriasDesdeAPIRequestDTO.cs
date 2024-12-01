using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{
    /// <summary>
    /// DTO del body del método para obtener categorías desde API
    /// </summary>
    public class ObtenerCategoriasDesdeAPIRequestDTO
    {
        /// <summary>
        /// Valor del enum que identifica a la API que se quiere consultar
        /// </summary>
        [JsonPropertyName("api")]
        public PaginasElegiblesEnum Api { get; set; }
    }
}
