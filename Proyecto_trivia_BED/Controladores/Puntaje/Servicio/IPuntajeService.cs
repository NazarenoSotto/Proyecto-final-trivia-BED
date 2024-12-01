using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Servicio
{
    /// <summary>
    /// Interfaz de PuntajeService
    /// </summary>
    public interface IPuntajeService
    {
        /// <summary>
        /// Calcular el puntaje del usuario
        /// </summary>
        /// <param name="request">CalculoPuntajeDTO</param>
        /// <returns>PuntajeDTO</returns>
        PuntajeDTO CalcularPuntaje(CalculoPuntajeDTO request);
        /// <summary>
        /// Obtener la lista de puntajes
        /// </summary>
        /// <returns>Lista de PuntajeDTO</returns>
        Task<List<PuntajeDTO>> ObtenerTodosLosPuntajes();

    }
}
