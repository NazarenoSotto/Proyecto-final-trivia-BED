using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.CTrivia.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.CTrivia.Servicio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controllers
{
    /// <summary>
    /// Controlador para endpoints de trivia (/trivia)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TriviaController : ControllerBase
    {
        private readonly ILogger<TriviaController> _logger;
        private readonly ITriviaService _triviaService;

        /// <summary>
        /// Constructor de TriviaController
        /// </summary>
        /// <param name="logger">ILogger<TriviaController></param>
        /// <param name="triviaService">ITriviaService</param>
        public TriviaController(ILogger<TriviaController> logger, ITriviaService triviaService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _triviaService = triviaService ?? throw new ArgumentNullException(nameof(triviaService));
        }

        /// <summary>
        /// /obtenerPreguntas: Obtener lista de preguntas
        /// </summary>
        /// <param name="cantidad">Cantidad de preguntas</param>
        /// <param name="categoriaId">Id de categoría</param>
        /// <param name="dificultadId">Id de dificultad</param>
        /// <returns>Lista de PreguntaDTO</returns>
        [HttpGet("obtenerPreguntas")]
        public async Task<IActionResult> ObtenerPreguntas([FromQuery] int cantidad, [FromQuery] int categoriaId, [FromQuery] int dificultadId)
        {
            try
            {
                var preguntas = await _triviaService.ObtenerPreguntas(categoriaId, dificultadId, cantidad);
                return Ok(preguntas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener preguntas: {ex.Message}");
                return StatusCode(500, $"Error al obtener preguntas: {ex.Message}");
            }
        }

        /// <summary>
        /// /obtenerPreguntasDesdeAPI: Obtener preguntas con los parámetros definidos desde la API elegida
        /// </summary>
        /// <param name="requestBody">ObtenerPreguntasDesdeAPIRequestDTO</param>
        /// <returns>Lista de PreguntaDTO</returns>
        [HttpPost("obtenerPreguntasDesdeAPI")]
        public async Task<IActionResult> ObtenerPreguntasDesdeAPI([FromBody] ObtenerPreguntasDesdeAPIRequestDTO requestBody)
        {
            try
            {
                List<PreguntaDTO> response = await _triviaService.ObtenerPreguntasDesdeAPIAsync(requestBody.Api, requestBody.Cantidad, requestBody.CategoriaId, requestBody.DificultadId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un problema al obtener las preguntas: {ex.Message}");
                return StatusCode(500, new { message = "Hubo un problema al obtener las preguntas.", details = ex.Message });
            }
        }

        /// <summary>
        /// /obtenerCategorias: Obtener lista de categorías
        /// </summary>
        /// <returns>Lista de CategoriaDTO</returns>
        [HttpGet("obtenerCategorias")]
        public async Task<IActionResult> ObtenerCategorias()
        {
            try
            {
                var categorias = await _triviaService.ObtenerCategorias(PaginasElegiblesEnum.OpenTDB);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener categorías: {ex.Message}");
                return StatusCode(500, $"Error al obtener categorías: {ex.Message}");
            }
        }

        /// <summary>
        /// /obtenerDificultades: Obtener dificultades de las preguntas
        /// </summary>
        /// <returns>Lista de DificultadDTO</returns>
        [HttpGet("obtenerDificultades")]
        public async Task<IActionResult> ObtenerDificultades()
        {
            try
            {
                var dificultades = await _triviaService.ObtenerDificultades(PaginasElegiblesEnum.OpenTDB);
                return Ok(dificultades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener dificultades: {ex.Message}");
                return StatusCode(500, $"Error al obtener dificultades: {ex.Message}");
            }
        }

        /// <summary>
        /// /agregarPreguntaManual: Guardar una pregunta manualmente
        /// </summary>
        /// <param name="pregunta">Pregunta a guardar</param>
        /// <returns>Booleano</returns>
        [HttpPost("agregarPreguntaManual")]
        public async Task<IActionResult> AgregarPreguntaManual([FromBody] PreguntaDTO pregunta)
        {
            try
            {
                var result = await _triviaService.AgregarPreguntaManual(pregunta);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar pregunta: {ex.Message}");
                return StatusCode(500, $"Error al guardar pregunta: {ex.Message}");
            }
        }

        /// <summary>
        /// /verificarPregunta: Verificar pregunta y sus respuestas
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a verificar</param>
        /// <returns>PreguntaDTO</returns>
        [HttpPost("verificarPregunta")]
        public async Task<IActionResult> VerificarPregunta([FromBody] PreguntaDTO preguntaDTO)
        {
            if (preguntaDTO == null)
            {
                return BadRequest("La solicitud no puede estar vacía.");
            }

            try
            {
                var resultado = await _triviaService.VerificarPregunta(preguntaDTO);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al verificar la pregunta: {ex.Message}");
                return StatusCode(500, $"Error al verificar la pregunta: {ex.Message}");
            }
        }

        /// <summary>
        /// /obtenerCategoriasDesdeAPI: Obtener categorías desde la API
        /// </summary>
        /// <param name="requestBody">ObtenerCategoriasDesdeAPIRequestDTO</param>
        /// <returns>Lista de CategoriaDTO</returns>
        [HttpPost("obtenerCategoriasDesdeAPI")]
        public async Task<IActionResult> ObtenerCategoriasDesdeAPI([FromBody] ObtenerCategoriasDesdeAPIRequestDTO requestBody)
        {
            try
            {
                if (!Enum.IsDefined(typeof(PaginasElegiblesEnum), requestBody.Api))
                {
                    throw new ArgumentException("Valor inválido en 'api'");
                }

                List<CategoriaDTO> response = await _triviaService.CargarCategoriasDesdeAPIAsync(requestBody.Api);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un problema al obtener las categorías: {ex.Message}");
                return StatusCode(500, new { message = "Hubo un problema al obtener las categorías.", details = ex.Message });
            }
        }
    }
}
