using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;
using System;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }


        [HttpPost("crear")]
        public IActionResult CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                _logger.LogWarning("Solicitud inválida: el cuerpo está vacío.");
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            try
            {
                if (_usuarioService.NombreUsuarioExistente(usuarioDTO.NombreUsuario))
                {
                    return Conflict("El nombre de usuario ya existe.");
                }

                var nuevoUsuario = _usuarioService.AgregarUsuario(usuarioDTO);
                return Ok(nuevoUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [HttpPost("autenticar")]
        public IActionResult AutenticarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null || string.IsNullOrWhiteSpace(usuarioDTO.NombreUsuario) || string.IsNullOrWhiteSpace(usuarioDTO.Password))
            {
                _logger.LogWarning("Solicitud de inicio de sesión inválida.");
                return BadRequest("Nombre de usuario y contraseña son obligatorios.");
            }

            try
            {
                var usuarioAutenticado = _usuarioService.AutenticarUsuario(usuarioDTO);

                if (usuarioAutenticado == null)
                {
                    return Unauthorized("Credenciales inválidas.");
                }

                return Ok(usuarioAutenticado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al autenticar el usuario.");
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }
    }
}
