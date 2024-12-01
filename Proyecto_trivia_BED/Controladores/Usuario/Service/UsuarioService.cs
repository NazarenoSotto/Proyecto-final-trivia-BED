using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;
using System;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    /// <summary>
    /// Servicio para las funcionalidades de usuario
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioModelo _usuarioModelo;

        /// <summary>
        /// Constructor de UsuarioService
        /// </summary>
        /// <param name="usuarioModelo">UsuarioModelo</param>
        public UsuarioService(UsuarioModelo usuarioModelo)
        {
            _usuarioModelo = usuarioModelo ?? throw new ArgumentNullException(nameof(usuarioModelo));
        }

        /// <summary>
        /// Agregar un usuario
        /// </summary>
        /// <param name="usuarioDTO">Usuario a agregar</param>
        /// <returns>UsuarioDTO</returns>
        public UsuarioDTO AgregarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));

            if (_usuarioModelo.NombreUsuarioExistente(usuarioDTO.NombreUsuario))
                throw new InvalidOperationException("El nombre de usuario ya existe.");

            usuarioDTO.Password = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Password);

            var usuarioEntidad = ConvertirAEntidad(usuarioDTO);
            var usuarioGuardado = _usuarioModelo.AgregarUsuario(usuarioEntidad);

            return ConvertirADTO(usuarioGuardado);
        }

        /// <summary>
        /// Convertir usuario entidad a usuario DTO
        /// </summary>
        /// <param name="usuario">usuario entidad a convertir</param>
        /// <returns>UsuarioDTO</returns>
        private UsuarioDTO ConvertirADTO(EUsuario usuario)
        {
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                EsAdmin = usuario.EsAdmin,
                Password = usuario.Password
            };
        }

        /// <summary>
        /// Convertir usuario DTO a usuario entidad
        /// </summary>
        /// <param name="usuarioDTO">Usuario DTO a convertir</param>
        /// <returns>EUsuario</returns>
        private EUsuario ConvertirAEntidad(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return null;

            return new EUsuario
            {
                IdUsuario = usuarioDTO.IdUsuario,
                NombreUsuario = usuarioDTO.NombreUsuario,
                EsAdmin = usuarioDTO.EsAdmin,
                Password = usuarioDTO.Password
            };
        }


        /// <summary>
        /// Verifica si el nombre del usuario ya existe
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <returns>Booleano</returns>
        public bool NombreUsuarioExistente(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(nombreUsuario));

            return _usuarioModelo.NombreUsuarioExistente(nombreUsuario);
        }

        /// <summary>
        /// Obtener usuario por nombre
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <returns>EUsuario</returns>
        private EUsuario ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            return _usuarioModelo.ObtenerUsuarioPorNombre(nombreUsuario);
        }

        /// <summary>
        /// Verifica si la password ingresada concuerda con la password almacenada encriptada
        /// </summary>
        /// <param name="passwordIngresada">Password ingresada</param>
        /// <param name="passwordAlmacenada">Password almacenada encriptada</param>
        /// <returns>Booleano</returns>
        private bool VerificarPassword(string passwordIngresada, string passwordAlmacenada)
        {
            return BCrypt.Net.BCrypt.Verify(passwordIngresada, passwordAlmacenada);
        }

        /// <summary>
        /// Obtener un usuario por Id
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>EUsuario</returns>
        public EUsuario ObtenerUsuarioPorId(int idUsuario)
        {
            return _usuarioModelo.ObtenerUsuarioPorId(idUsuario);
        }

        /// <summary>
        /// Autenticar al usuario con su nombre y contraseña
        /// </summary>
        /// <param name="usuarioDTO">Usuario a autenticar</param>
        /// <returns>UsuarioDTO</returns>
        public UsuarioDTO AutenticarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));

            var usuarioEntidad = ObtenerUsuarioPorNombre(usuarioDTO.NombreUsuario);
            if (usuarioEntidad == null)
                return null;

            bool passwordValido = VerificarPassword(usuarioDTO.Password, usuarioEntidad.Password);
            if (!passwordValido)
                return null;

            return ConvertirADTO(usuarioEntidad);
        }
    }
}
