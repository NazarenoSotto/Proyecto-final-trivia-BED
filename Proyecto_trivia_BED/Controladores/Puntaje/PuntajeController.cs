using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.Puntaje.Servicio;

namespace Proyecto_trivia_BED.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PuntajeController : ControllerBase
    {
        private readonly IPuntajeService _puntajeService;

        public PuntajeController(IPuntajeService puntajeService)
        {
            _puntajeService = puntajeService ?? throw new ArgumentNullException(nameof(puntajeService));
        }

        [HttpPost("calcular")]
        public IActionResult CalcularPuntaje([FromBody] CalculoPuntajeDTO request)
        {
            if (request == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            try
            {
                var puntaje = _puntajeService.CalcularPuntaje(request);
                return Ok(puntaje);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al calcular el puntaje: {ex.Message}");
            }
        }

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
                return StatusCode(500, $"Error al obtener los puntajes: {ex.Message}");
            }
        }
    }
}
