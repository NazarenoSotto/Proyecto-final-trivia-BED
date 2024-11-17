using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{
    public class ObtenerCategoriasDesdeAPIRequestDTO
    {
        [JsonPropertyName("api")]
        public PaginasElegiblesEnum Api { get; set; }
    }
}
