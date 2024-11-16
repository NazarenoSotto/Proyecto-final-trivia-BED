using System;
using Microsoft.AspNetCore.Mvc;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.Puntaje.Servicio;

namespace Proyecto_trivia_BED.Controladores.Puntaje
{
    [ApiController]
    [Route("[controller]")]
    public class PuntajeController : ControllerBase
    {
        private readonly IPuntajeServicio _puntajeServicio;

        public PuntajeController(IPuntajeServicio puntajeServicio)
        {
            _puntajeServicio = puntajeServicio ?? throw new ArgumentNullException(nameof(puntajeServicio));
        }

        [HttpPost("calcular")]
        public IActionResult CalcularPuntaje([FromBody] PuntajeRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            try
            {
                var puntaje = _puntajeServicio.CalcularPuntaje(request);
                return Ok(puntaje);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al calcular el puntaje: {ex.Message}");
            }
        }

        [HttpGet("obtener")]
        public IActionResult ObtenerPuntajes()
        {
            try
            {
                var puntajes = _puntajeServicio.ObtenerTodosLosPuntajes();
                return Ok(puntajes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los puntajes: {ex.Message}");
            }
        }
    }
}
