using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.CUsuario.Modelo.DTO;
using Proyecto_trivia_BED.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CUsuario.Modelo
{
    /// <summary>
    /// Servicio para las funcionalidades de usuario
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly EntityRepository<Usuario> _usuarioRepositorio;

        /// <summary>
        /// Constructor de UsuarioService
        /// </summary>
        /// <param name="usuarioRepositorio">EntityRepository<ContextoDB.Entidad.Usuario></param>
        public UsuarioService(EntityRepository<Usuario> usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
        }

        /// <summary>
        /// Agregar un usuario
        /// </summary>
        /// <param name="usuarioDTO">Usuario a agregar</param>
        /// <returns>UsuarioDTO</returns>
        public async Task<UsuarioDTO> AgregarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));
            IEnumerable<Usuario> usuarioExistenteEnumerable = await _usuarioRepositorio.GetAsync(usuario => usuario.NombreUsuario == usuarioDTO.NombreUsuario);
            if (usuarioExistenteList.)
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
        private UsuarioDTO ConvertirADTO(ContextoDB.Entidad.Usuario usuario)
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
        /// <returns>Usuario</returns>
        private ContextoDB.Entidad.Usuario ConvertirAEntidad(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return null;

            return new ContextoDB.Entidad.Usuario
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
        /// <returns>Usuario</returns>
        private ContextoDB.Entidad.Usuario ObtenerUsuarioPorNombre(string nombreUsuario)
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
        /// <returns>Usuario</returns>
        public ContextoDB.Entidad.Usuario ObtenerUsuarioPorId(int idUsuario)
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
