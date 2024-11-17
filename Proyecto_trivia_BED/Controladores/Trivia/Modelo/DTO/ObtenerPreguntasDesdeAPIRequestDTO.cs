using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{   public class ObtenerPreguntasDesdeAPIRequestDTO
    {
        public PaginasElegiblesEnum Api { get; set; }
        public int Cantidad { get; set; }
        public int? CategoriaId { get; set; }
        public int? DificultadId { get; set; }
    }
}
