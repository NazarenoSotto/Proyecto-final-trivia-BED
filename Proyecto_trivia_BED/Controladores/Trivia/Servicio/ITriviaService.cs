using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Servicio
{
    /// <summary>
    /// Interfaz de servicio para funcionalidades relacionadas con trivia
    /// </summary>
    public interface ITriviaService
    {
        /// <summary>
        /// Obtener lista de preguntas
        /// </summary>
        /// <param name="categoriaId">Id de categoría</param>
        /// <param name="dificultadId">Id de dificultad</param>
        /// <param name="cantidad">Cantidad de preguntas</param>
        /// <returns>Lista de PreguntaDTO</returns>
        Task<List<PreguntaDTO>> ObtenerPreguntas(int categoriaId, int dificultadId, int cantidad);

        /// <summary>
        /// Guardar una pregunta manualmente
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a guardar</param>
        /// <returns>Booleano</returns>
        Task<bool> GuardarPreguntaManual(PreguntaDTO preguntaDTO);

        /// <summary>
        /// Verificar pregunta y sus respuestas
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a verificar</param>
        /// <returns>PreguntaDTO</returns>
        Task<PreguntaDTO> VerificarPregunta(PreguntaDTO preguntaDTO);
    }
}
