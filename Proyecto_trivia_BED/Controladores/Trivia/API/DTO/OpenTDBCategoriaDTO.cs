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
        public List<OpenTDBCategoriaDTO> trivia_categories{ get; set; }
        public class OpenTDBCategoriaDTO
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }
}
