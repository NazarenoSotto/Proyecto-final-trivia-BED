using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Servicio
{
    public interface IPuntajeService
    {
        PuntajeDTO CalcularPuntaje(CalculoPuntajeDTO request);
        Task<List<PuntajeDTO>> ObtenerTodosLosPuntajes();

    }
}
