using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using System.Data.Entity;

namespace Proyecto_trivia_BED.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TriviaControlador : ControllerBase
    {
        private readonly ILogger<TriviaControlador> _logger;

        private readonly TriviaContext _context;
        public TriviaControlador(ILogger<TriviaControlador> logger, TriviaContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> NuevoUsuario()
        {
            try
            {
                var dificultades = await _context.Dificultades.ToListAsync();

                return Ok(dificultades);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un problema al obtener las preguntas.", details = ex.Message });
            }
            
        }
    }
}
