using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.API.DTO
{
    public class OpenTDBCategoriaResponseDTO
    {
        [JsonProperty("trivia_categories")]
        public List<OpenTDBCategoriaDTO> TriviaCategories { get; set; }
        public class OpenTDBCategoriaDTO
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
