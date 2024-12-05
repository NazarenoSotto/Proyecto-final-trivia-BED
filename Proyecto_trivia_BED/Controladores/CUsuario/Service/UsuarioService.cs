using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.CUsuario.Modelo.DTO;
using Proyecto_trivia_BED.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CUsuario.Modelo
{
    /// <summary>
    /// Servicio para las funcionalidades de usuario
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly IEntityRepository<Usuario> _usuarioRepositorio;

        /// <summary>
        /// Constructor de UsuarioService
        /// </summary>
        /// <param name="usuarioRepositorio">Repositorio de usuarios</param>
        public UsuarioService(IEntityRepository<Usuario> usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
        }

        /// <summary>
        /// Agregar un usuario
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a agregar</param>
        /// <returns>UsuarioDTO</returns>
        public async Task<UsuarioDTO> AgregarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));

            var existe = await _usuarioRepositorio.GetAsync(u => u.NombreUsuario == usuarioDTO.NombreUsuario);
            if (existe.Any())
                throw new InvalidOperationException("El nombre de usuario ya existe.");

            usuarioDTO.Password = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Password);

            var usuarioEntidad = ConvertirAEntidad(usuarioDTO);
            await _usuarioRepositorio.CreateAsync(usuarioEntidad);
            await _usuarioRepositorio.SaveChangesAsync();

            return ConvertirADTO(usuarioEntidad);
        }

        /// <summary>
        /// Verifica si el nombre del usuario ya existe
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <returns>Booleano</returns>
        public async Task<bool> NombreUsuarioExistente(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(nombreUsuario));

            var existe = await _usuarioRepositorio.GetAsync(u => u.NombreUsuario == nombreUsuario);
            return existe.Any();
        }

        /// <summary>
        /// Obtener un usuario por Id
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>UsuarioDTO</returns>
        public async Task<UsuarioDTO> ObtenerUsuarioPorId(int idUsuario)
        {
            var usuario = await _usuarioRepositorio.GetByIdAsync(idUsuario);
            return ConvertirADTO(usuario);
        }

        /// <summary>
        /// Autenticar al usuario con su nombre y contraseña
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a autenticar</param>
        /// <returns>UsuarioDTO</returns>
        public async Task<UsuarioDTO> AutenticarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));

            var usuarios = await _usuarioRepositorio.GetAsync(u => u.NombreUsuario == usuarioDTO.NombreUsuario);
            var usuarioEntidad = usuarios.FirstOrDefault();

            if (usuarioEntidad == null || !BCrypt.Net.BCrypt.Verify(usuarioDTO.Password, usuarioEntidad.Password))
                return null;

            return ConvertirADTO(usuarioEntidad);
        }

        /// <summary>
        /// Convertir usuario entidad a usuario DTO
        /// </summary>
        /// <param name="usuario">Usuario entidad a convertir</param>
        /// <returns>UsuarioDTO</returns>
        private UsuarioDTO ConvertirADTO(Usuario usuario)
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
        private Usuario ConvertirAEntidad(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return null;

            return new Usuario
            {
                IdUsuario = usuarioDTO.IdUsuario,
                NombreUsuario = usuarioDTO.NombreUsuario,
                EsAdmin = usuarioDTO.EsAdmin,
                Password = usuarioDTO.Password
            };
        }
    }
}
