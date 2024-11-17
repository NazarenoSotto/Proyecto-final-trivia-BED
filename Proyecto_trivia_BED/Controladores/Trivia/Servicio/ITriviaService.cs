using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Servicio
{
    public interface ITriviaService
    {
        public Task<List<PreguntaDTO>> ObtenerPreguntasDesdeAPIAsync(PaginasElegiblesEnum apiEnum, int cantidad, int? categoriaId, int? dificultadId);
        public List<CategoriaDTO> ObtenerCategorias();
        public List<DificultadDTO> ObtenerDificultades();
        public List<PreguntaDTO> ObtenerPreguntas(PreguntaRequestDTO request);
        public bool GuardarPreguntaManual(PreguntaDTO pregunta);
        public Task<List<CategoriaDTO>> CargarCategoriasDesdeAPIAsync(PaginasElegiblesEnum apiEnum);
    }
}
