using Newtonsoft.Json;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{   public class ObtenerPreguntasDesdeAPIRequestDTO
    {
        [JsonProperty("api")]
        public PaginasElegiblesEnum Api { get; set; }
        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }
        [JsonProperty("categoriaId")]
        public int? CategoriaId { get; set; }
        [JsonProperty("dificultadId")]
        public int? DificultadId { get; set; }
    }
}
