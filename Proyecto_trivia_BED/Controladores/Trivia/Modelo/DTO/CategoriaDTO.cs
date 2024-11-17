using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{
    public class CategoriaDTO
    {
        public int IdCategoria{ get; set; }

        public string NombreCategoria { get; set; }

        public int WebId { get; set; }

    }
}
