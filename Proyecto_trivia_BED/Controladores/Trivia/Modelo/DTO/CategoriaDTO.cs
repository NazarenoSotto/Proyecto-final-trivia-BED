using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO
{
    public class CategoriaDTO
    {
        public int IdCategoria{ get; set; }

        public string NombreCategoria { get; set; }

        public bool WebId { get; set; }

    }
}
