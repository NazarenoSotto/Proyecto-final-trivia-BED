using Proyecto_trivia_BED.ContextoDB.Entidad;
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
        Task<bool> AgregarPreguntaManual(PreguntaDTO preguntaDTO);

        /// <summary>
        /// Verificar pregunta y sus respuestas
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a verificar</param>
        /// <returns>PreguntaDTO</returns>
        Task<PreguntaDTO> VerificarPregunta(PreguntaDTO preguntaDTO);

        /// <summary>
        /// Obtener preguntas desde la API externa
        /// </summary>
        /// <param name="api">Enum de API externa</param>
        /// <param name="cantidad">Cantidad de preguntas</param>
        /// <param name="categoriaId">Id de categoría</param>
        /// <param name="dificultadId">Id de dificultad</param>
        /// <returns>Lista de PreguntaDTO</returns>
        Task<List<PreguntaDTO>> ObtenerPreguntasDesdeAPIAsync(PaginasElegiblesEnum api, int cantidad, int? categoriaId, int? dificultadId);

        /// <summary>
        /// Obtener lista de categorías desde la base de datos
        /// </summary>
        /// <param name="api">Enum de API externa</param>
        /// <returns>Lista de CategoriaDTO</returns>
        Task<List<CategoriaDTO>> ObtenerCategorias(PaginasElegiblesEnum api);

        /// <summary>
        /// Cargar categorías desde la API externa
        /// </summary>
        /// <param name="api">Enum de API externa</param>
        /// <returns>Lista de CategoriaDTO</returns>
        Task<List<CategoriaDTO>> CargarCategoriasDesdeAPIAsync(PaginasElegiblesEnum api);

        /// <summary>
        /// Obtener lista de dificultades desde la base de datos
        /// </summary>
        /// <param name="api">Enum de API externa</param>
        /// <returns>Lista de DificultadDTO</returns>
        Task<List<DificultadDTO>> ObtenerDificultades(PaginasElegiblesEnum api);
    }
}
