using Newtonsoft.Json;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CTrivia.Modelo.DTO
{
    /// <summary>
    /// DTO del body del método para obtener preguntas desde API
    /// </summary>
    public class ObtenerPreguntasDesdeAPIRequestDTO
    {
        /// <summary>
        /// Valor del enum que identifica a la API que se quiere consultar
        /// </summary>
        [JsonProperty("api")]
        public PaginasElegiblesEnum Api { get; set; }
        /// <summary>
        /// Cantidad de preguntas a obtener
        /// </summary>
        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }
        /// <summary>
        /// Id de la categoría de las preguntas
        /// </summary>
        [JsonProperty("categoriaId")]
        public int? CategoriaId { get; set; }
        /// <summary>
        /// Id de la dificultad de las preguntas
        /// </summary>
        [JsonProperty("dificultadId")]
        public int? DificultadId { get; set; }
    }
}
