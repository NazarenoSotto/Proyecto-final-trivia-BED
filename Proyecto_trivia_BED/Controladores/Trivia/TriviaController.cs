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
    [ApiController]
    [Route("[controller]")]
    public class TriviaController : ControllerBase
    {
        private readonly ILogger<TriviaController> _logger;


        private readonly ITriviaService _triviaService;

        public TriviaController(ILogger<TriviaController> logger, ITriviaService triviaService )
        {
            _logger = logger;
            _triviaService = triviaService ?? throw new ArgumentNullException(nameof(triviaService));
        }

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
                return StatusCode(500, new { message = "Hubo un problema al obtener las preguntas.", details = ex.Message });
            }

        }

        [HttpGet("obtenerCategorias")]
        public IActionResult ObtenerCategorias()
        {
            try
            {
                var categorias = _triviaService.ObtenerCategorias();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener categorías: {ex.Message}");
            }
        }

        [HttpGet("obtenerDificultades")]
        public IActionResult ObtenerDificultades()
        {
            try
            {
                var dificultades = _triviaService.ObtenerDificultades();
                return Ok(dificultades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener dificultades: {ex.Message}");
            }
        }

        [HttpPost("obtenerCategoriasDesdeAPI")]
        public async Task<IActionResult> ObtenerCategoriasDesdeAPI([FromBody] ObtenerCategoriasDesdeAPIRequestDTO requestBody)
        {
            try
            {
                _logger.LogInformation($"requestBody.Api: {requestBody.Api.ToString()} aaaa");
                if (!Enum.IsDefined(typeof(PaginasElegiblesEnum), requestBody.Api)) {
                    throw new ArgumentException("Valor inválido en 'api'");
                }
                List<CategoriaDTO> response = await _triviaService.CargarCategoriasDesdeAPIAsync(requestBody.Api);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un problema al obtener las categorias.", details = ex.Message });
            }

        }
    }
}
