using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{
    public class DificultadDTO
    {
        public int IdDificultad { get; set; }
        public string NombreDificultad { get; set; }

        public int Valor { get; set; }

        public int webId { get; set; }

    }
}