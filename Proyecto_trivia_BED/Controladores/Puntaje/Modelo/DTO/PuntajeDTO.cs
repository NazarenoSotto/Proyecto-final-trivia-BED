using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;
using System;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO
{
    public class PuntajeDTO
    {
        public int IdPuntaje { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public float ValorPuntaje { get; set; }
        public DateTime Fecha { get; set; }
        public int Tiempo { get; set; }
        public int CantidadPreguntas { get; set; }
        public int CantidadCorrectas { get; set; }

    }
}
