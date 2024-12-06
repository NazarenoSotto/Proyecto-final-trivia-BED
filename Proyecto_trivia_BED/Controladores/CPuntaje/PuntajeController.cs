using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_trivia_BED.Controladores.CPuntaje.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.CPuntaje.Servicio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controllers
{
    /// <summary>
    /// Controlador para endpoints de puntaje (/puntaje)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PuntajeController : ControllerBase
    {
        private readonly IPuntajeService _puntajeService;
        private readonly ILogger<PuntajeController> _logger;

        /// <summary>
        /// Constructor de PuntajeController
        /// </summary>
        /// <param name="puntajeService">IPuntajeService</param>
        /// <param name="logger">ILogger<PuntajeController></param>
        public PuntajeController(IPuntajeService puntajeService, ILogger<PuntajeController> logger)
        {
            _puntajeService = puntajeService ?? throw new ArgumentNullException(nameof(puntajeService));
            _logger = logger;
        }

        /// <summary>
        /// /calcular: Calcular el puntaje del usuario
        /// </summary>
        /// <param name="request">Datos necesarios para el cálculo</param>
        /// <returns>PuntajeDTO</returns>
        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularPuntaje([FromBody] CalculoPuntajeDTO request)
        {
            if (request == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            try
            {
                var puntaje = await _puntajeService.CalcularPuntaje(request);
                return Ok(puntaje);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al calcular el puntaje: {ex.Message}");
                return StatusCode(500, $"Error al calcular el puntaje: {ex.Message}");
            }
        }

        /// <summary>
        /// /obtener: Obtener lista de puntajes
        /// </summary>
        /// <returns>Lista de PuntajeDTO</returns>
        [HttpGet("obtener")]
        public async Task<IActionResult> ObtenerPuntajes()
        {
            try
            {
                var puntajes = await _puntajeService.ObtenerTodosLosPuntajes();
                return Ok(puntajes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los puntajes: {ex.Message}");
                return StatusCode(500, $"Error al obtener los puntajes: {ex.Message}");
            }
        }
    }
}
