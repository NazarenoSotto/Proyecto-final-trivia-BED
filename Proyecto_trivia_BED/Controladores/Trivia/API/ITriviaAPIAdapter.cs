using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.API.DTO
{
    /// <summary>
    /// Interfaz de adapter de APIs de trivia
    /// </summary>
    public interface ITriviaAPIAdapter
    {
        /// <summary>
        /// Obtener preguntas desde la API
        /// </summary>
        /// <param name="cantidad">Cantidad de preguntas a obtener</param>
        /// <param name="categoriaId">Categoría de las preguntas</param>
        /// <param name="dificultadId">Dificultades de las preguntas</param>
        /// <returns>Lista de preguntas</returns>
        public Task<List<Pregunta>> ObtenerPreguntasAsync(int cantidad, int? categoriaId, int? dificultadId);
        
        /// <summary>
        /// Obtener categorías desde la  API
        /// </summary>
        /// <returns>Lista de ECategoría</returns>
        public Task<List<Categoria>> ObtenerCategoriasAsync();
    }
}
