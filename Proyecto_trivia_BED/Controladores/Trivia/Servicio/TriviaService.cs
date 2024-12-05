using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using Proyecto_trivia_BED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Servicio
{
    /// <summary>
    /// Servicio para las funcionalidades de trivia
    /// </summary>
    public class TriviaService : ITriviaService
    {
        private readonly IEntityRepository<Pregunta> _preguntaRepositorio;
        private readonly IEntityRepository<Categoria> _categoriaRepositorio;
        private readonly IEntityRepository<Dificultad> _dificultadRepositorio;

        /// <summary>
        /// Constructor de TriviaService
        /// </summary>
        /// <param name="preguntaRepositorio">Repositorio de preguntas</param>
        /// <param name="categoriaRepositorio">Repositorio de categorías</param>
        /// <param name="dificultadRepositorio">Repositorio de dificultades</param>
        public TriviaService(
            IEntityRepository<Pregunta> preguntaRepositorio,
            IEntityRepository<Categoria> categoriaRepositorio,
            IEntityRepository<Dificultad> dificultadRepositorio)
        {
            _preguntaRepositorio = preguntaRepositorio ?? throw new ArgumentNullException(nameof(preguntaRepositorio));
            _categoriaRepositorio = categoriaRepositorio ?? throw new ArgumentNullException(nameof(categoriaRepositorio));
            _dificultadRepositorio = dificultadRepositorio ?? throw new ArgumentNullException(nameof(dificultadRepositorio));
        }

        /// <summary>
        /// Obtener lista de preguntas
        /// </summary>
        /// <param name="categoriaId">Id de categoría</param>
        /// <param name="dificultadId">Id de dificultad</param>
        /// <param name="cantidad">Cantidad de preguntas</param>
        /// <returns>Lista de PreguntaDTO</returns>
        public async Task<List<PreguntaDTO>> ObtenerPreguntas(int categoriaId, int dificultadId, int cantidad)
        {
            var preguntas = await _preguntaRepositorio.GetAsync(
                p => p.Categoria.IdCategoria == categoriaId && p.Dificultad.IdDificultad == dificultadId
            );

            return preguntas.Take(cantidad).Select(MapearPreguntaADTO).ToList();
        }

        /// <summary>
        /// Guardar una pregunta manualmente
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a guardar</param>
        /// <returns>Booleano</returns>
        public async Task<bool> GuardarPreguntaManual(PreguntaDTO preguntaDTO)
        {
            var categoria = await _categoriaRepositorio.GetByIdAsync(preguntaDTO.Categoria.IdCategoria);
            var dificultad = await _dificultadRepositorio.GetByIdAsync(preguntaDTO.Dificultad.IdDificultad);

            if (categoria == null || dificultad == null)
                throw new InvalidOperationException("Categoría o dificultad no encontrada.");

            var preguntaEntidad = new Pregunta
            {
                LaPregunta = preguntaDTO.LaPregunta,
                Categoria = categoria,
                Dificultad = dificultad,
                Respuestas = preguntaDTO.Respuestas.Select(r => new Respuesta
                {
                    SRespuesta = r.TextoRespuesta,
                    Correcta = r.Correcta
                }).ToList()
            };

            await _preguntaRepositorio.CreateAsync(preguntaEntidad);
            await _preguntaRepositorio.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Verificar pregunta y sus respuestas
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a verificar</param>
        /// <returns>PreguntaDTO</returns>
        public async Task<PreguntaDTO> VerificarPregunta(PreguntaDTO preguntaDTO)
        {
            var pregunta = await _preguntaRepositorio.GetByIdAsync(preguntaDTO.IdPregunta);

            if (pregunta == null)
                throw new InvalidOperationException("Pregunta no encontrada.");

            var respuestaCorrectaId = pregunta.Respuestas.FirstOrDefault(r => r.Correcta)?.IdRespuesta;

            foreach (var respuesta in preguntaDTO.Respuestas)
            {
                respuesta.Correcta = respuesta.IdRespuesta == respuestaCorrectaId;
            }

            return MapearPreguntaADTO(pregunta);
        }

        /// <summary>
        /// Convertir una pregunta entidad a DTO
        /// </summary>
        /// <param name="pregunta">Pregunta entidad a mapear</param>
        /// <returns>PreguntaDTO</returns>
        private PreguntaDTO MapearPreguntaADTO(Pregunta pregunta)
        {
            return new PreguntaDTO
            {
                IdPregunta = pregunta.IdPregunta,
                LaPregunta = pregunta.LaPregunta,
                Categoria = new CategoriaDTO
                {
                    IdCategoria = pregunta.Categoria.IdCategoria,
                    NombreCategoria = pregunta.Categoria.NombreCategoria
                },
                Dificultad = new DificultadDTO
                {
                    IdDificultad = pregunta.Dificultad.IdDificultad,
                    NombreDificultad = pregunta.Dificultad.NombreDificultad
                },
                Respuestas = pregunta.Respuestas.Select(r => new RespuestaDTO
                {
                    IdRespuesta = r.IdRespuesta,
                    TextoRespuesta = r.SRespuesta,
                    Correcta = r.Correcta
                }).ToList()
            };
        }
    }
}
