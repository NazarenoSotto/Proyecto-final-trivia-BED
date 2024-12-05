using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.Trivia.Servicio;
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
            _logger = logger;
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
        /// /guardarPreguntaManual: Guardar una pregunta manualmente
        /// </summary>
        /// <param name="pregunta">Pregunta a guardar</param>
        /// <returns>Booleano</returns>
        [HttpPost("guardarPreguntaManual")]
        public async Task<IActionResult> GuardarPreguntaManual([FromBody] PreguntaDTO pregunta)
        {
            try
            {
                var result = await _triviaService.GuardarPreguntaManual(pregunta);
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
    }
}
