using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.API.DTO
{
    public class OpenTDBCategoriaResponseDTO
    {
        public List<OpenTDBCategoriaDTO> TriviaCategories { get; set; }
        public class OpenTDBCategoriaDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
