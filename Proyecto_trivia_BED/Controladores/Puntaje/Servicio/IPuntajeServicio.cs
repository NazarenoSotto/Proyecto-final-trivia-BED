using System.Collections.Generic;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Servicio
{
    public interface IPuntajeServicio
    {
        PuntajeDTO CalcularPuntaje(PuntajeRequestDTO request);
        List<PuntajeDTO> ObtenerTodosLosPuntajes();

    }
}
