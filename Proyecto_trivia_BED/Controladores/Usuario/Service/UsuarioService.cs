using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;
using System;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioModelo _usuarioModelo;

        public UsuarioService(UsuarioModelo usuarioModelo)
        {
            _usuarioModelo = usuarioModelo ?? throw new ArgumentNullException(nameof(usuarioModelo));
        }

        public UsuarioDTO AgregarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));
            if (_usuarioModelo.NombreUsuarioExistente(usuarioDTO.NombreUsuario))
            {
                throw new InvalidOperationException("El nombre de usuario ya existe.");
            }
            var usuarioEntidad = ConvertirAEntidad(usuarioDTO);
            var usuarioGuardado = _usuarioModelo.AgregarUsuario(usuarioEntidad);
            return ConvertirADTO(usuarioGuardado);
        }

        public UsuarioDTO ConvertirADTO(EUsuario usuario)
        {
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                EsAdmin = usuario.EsAdmin
            };
        }

        public EUsuario ConvertirAEntidad(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return null;

            return new EUsuario
            {
                IdUsuario = usuarioDTO.IdUsuario,
                NombreUsuario = usuarioDTO.NombreUsuario,
                EsAdmin = usuarioDTO.EsAdmin
            };
        }

        public bool NombreUsuarioExistente(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(nombreUsuario));

            return _usuarioModelo.NombreUsuarioExistente(nombreUsuario);
        }
    }
}
