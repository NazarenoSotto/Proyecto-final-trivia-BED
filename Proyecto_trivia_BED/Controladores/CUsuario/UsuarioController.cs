﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_trivia_BED.Controladores.CUsuario.Modelo;
using Proyecto_trivia_BED.Controladores.CUsuario.Modelo.DTO;
using System;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controllers
{
    /// <summary>
    /// Controlador para endpoints de usuario (/usuario)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Constructor de UsuarioController
        /// </summary>
        /// <param name="logger">ILogger<UsuarioController></param>
        /// <param name="usuarioService">IUsuarioService</param>
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        /// <summary>
        /// /crear: Crear/Registrar un nuevo usuario
        /// </summary>
        /// <param name="usuarioDTO">UsuarioDTO</param>
        /// <returns>UsuarioDTO</returns>
        [HttpPost("crear")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                _logger.LogWarning("Solicitud inválida: el cuerpo está vacío.");
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            try
            {
                if (await _usuarioService.NombreUsuarioExistente(usuarioDTO.NombreUsuario))
                {
                    return Conflict("El nombre de usuario ya existe.");
                }

                var nuevoUsuario = await _usuarioService.AgregarUsuario(usuarioDTO);
                return Ok(nuevoUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        /// <summary>
        /// /autenticar: Autenticar a un usuario
        /// </summary>
        /// <param name="usuarioDTO">UsuarioDTO</param>
        /// <returns>UsuarioDTO</returns>
        [HttpPost("autenticar")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null || string.IsNullOrWhiteSpace(usuarioDTO.NombreUsuario) || string.IsNullOrWhiteSpace(usuarioDTO.Password))
            {
                _logger.LogWarning("Solicitud de inicio de sesión inválida.");
                return BadRequest("Nombre de usuario y contraseña son obligatorios.");
            }

            try
            {
                var usuarioAutenticado = await _usuarioService.AutenticarUsuario(usuarioDTO);

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
