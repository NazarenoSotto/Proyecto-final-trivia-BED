using Proyecto_trivia_BED.Controladores.CPuntaje.Modelo.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CPuntaje.Servicio
{
    /// <summary>
    /// Interfaz de servicio para funcionalidades relacionadas con puntajes
    /// </summary>
    public interface IPuntajeService
    {
        /// <summary>
        /// Calcular el puntaje del usuario
        /// </summary>
        /// <param name="request">Datos necesarios para calcular el puntaje</param>
        /// <returns>PuntajeDTO</returns>
        Task<PuntajeDTO> CalcularPuntaje(CalculoPuntajeDTO request);

        /// <summary>
        /// Obtener la lista de puntajes
        /// </summary>
        /// <returns>Lista de PuntajeDTO</returns>
        Task<List<PuntajeDTO>> ObtenerTodosLosPuntajes();
    }
}
