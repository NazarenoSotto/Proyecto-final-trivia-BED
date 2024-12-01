using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using System.Data.Entity;
using Proyecto_trivia_BED.Controladores.Trivia.Servicio;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using Proyecto_trivia_BED.ContextoDB.Entidad;

namespace Proyecto_trivia_BED.Controllers
{
    /// <summary>
    /// Controlador para endpoints de la trivia (/trivia)
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
        public TriviaController(ILogger<TriviaController> logger, ITriviaService triviaService )
        {
            _logger = logger;
            _triviaService = triviaService ?? throw new ArgumentNullException(nameof(triviaService));
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
                _logger.LogError($"Hubo un problema al obtener las preguntas: ${ex.Message}");
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
                _logger.LogError($"Error al obtener categorías: ${ex.Message}");
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
                var preguntas = await _triviaService.ObtenerPreguntas(new PreguntaRequestDTO{ Cantidad = cantidad, CategoriaId = categoriaId, DificultadId = dificultadId});
                return Ok(preguntas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener preguntas: {ex.Message}");
                return StatusCode(500, $"Error al obtener preguntas: {ex.Message}");
            }
        }

        /// <summary>
        /// /verificarPregunta: Verificar pregunta y sus respuestas
        /// </summary>
        /// <param name="preguntaDTO"></param>
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
        /// /agregarPreguntaManual: Agregar pregunta manual
        /// </summary>
        /// <param name="pregunta">PreguntaDTO</param>
        /// <returns></returns>
        [HttpPost("agregarPreguntaManual")]
        public async Task<IActionResult> AgregarPreguntaManual([FromBody] PreguntaDTO pregunta)
        {
            try
            {
                await _triviaService.GuardarPreguntaManual(pregunta, PaginasElegiblesEnum.OpenTDB);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al agregar pregunta: {ex.Message}");
                return StatusCode(500, $"Error al agregar pregunta: {ex.Message}");
            }
        }

        /// <summary>
        /// /obtenerCategoriasDesdeAPI: Obtener categorias desde la API
        /// </summary>
        /// <param name="requestBody">ObtenerCategoriasDesdeAPIRequestDTO</param>
        /// <returns>Lista de CategoriaDTO</returns>
        [HttpPost("obtenerCategoriasDesdeAPI")]
        public async Task<IActionResult> ObtenerCategoriasDesdeAPI([FromBody] ObtenerCategoriasDesdeAPIRequestDTO requestBody)
        {
            try
            {
                if (!Enum.IsDefined(typeof(PaginasElegiblesEnum), requestBody.Api)) {
                    throw new ArgumentException("Valor inválido en 'api'");
                }
                List<CategoriaDTO> response = await _triviaService.CargarCategoriasDesdeAPIAsync(requestBody.Api);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un problema al obtener las categorias: {ex.Message}");
                return StatusCode(500, new { message = "Hubo un problema al obtener las categorias.", details = ex.Message });
            }

        }
    }
}
