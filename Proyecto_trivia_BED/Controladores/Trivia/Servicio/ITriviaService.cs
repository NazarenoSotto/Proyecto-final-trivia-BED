using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Servicio
{
    public interface ITriviaService
    {
        /// <summary>
        /// Obtener preguntas desde la api
        /// </summary>
        /// <param name="apiEnum">Enumerable que representa la API</param>
        /// <param name="cantidad">Cantidad de preguntas</param>
        /// <param name="categoriaId">Categoría de las preguntas</param>
        /// <param name="dificultadId">Dificultad de las preguntas</param>
        /// <returns>Lista de PreguntaDTO</returns>
        public Task<List<PreguntaDTO>> ObtenerPreguntasDesdeAPIAsync(PaginasElegiblesEnum apiEnum, int cantidad, int? categoriaId, int? dificultadId);
        /// <summary>
        /// Obtener lista de categorías
        /// </summary>
        /// <param name="api">Enumerable que representa la API</param>
        /// <returns>Lista de CategoriaDTO</returns>
        public Task<List<CategoriaDTO>> ObtenerCategorias(PaginasElegiblesEnum api);
        /// <summary>
        /// Obtener lista de dificultades
        /// </summary>
        /// <param name="api">Enumerable que representa la API</param>
        /// <returns></returns>
        public Task<List<DificultadDTO>> ObtenerDificultades(PaginasElegiblesEnum api);
        /// <summary>
        /// Obtener lista de preguntas
        /// </summary>
        /// <param name="request">PreguntaRequestDTO</param>
        /// <returns>Lista de PreguntaDTO</returns>
        public Task<List<PreguntaDTO>> ObtenerPreguntas(PreguntaRequestDTO request);
        /// <summary>
        /// Verificar pregunta
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a verificar</param>
        /// <returns>PreguntaDTO</returns>
        public Task<PreguntaDTO> VerificarPregunta(PreguntaDTO preguntaDTO);
        /// <summary>
        /// Guardar pregunta manual
        /// </summary>
        /// <param name="pregunta">Pregunta a guardar</param>
        /// <param name="api">Enumerable que representa la API</param>
        /// <returns>Booleano</returns>
        public Task<bool> GuardarPreguntaManual(PreguntaDTO pregunta, PaginasElegiblesEnum api);
        /// <summary>
        /// Cargar categorías desde la API seleccionada
        /// </summary>
        /// <param name="apiEnum">Enumerable que representa la API</param>
        /// <returns>Lista de CategoriaDTO</returns>
        public Task<List<CategoriaDTO>> CargarCategoriasDesdeAPIAsync(PaginasElegiblesEnum apiEnum);
    }
}
