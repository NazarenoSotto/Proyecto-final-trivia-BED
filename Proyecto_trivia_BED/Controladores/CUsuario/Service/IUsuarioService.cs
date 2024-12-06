using Proyecto_trivia_BED.Controladores.CUsuario.Modelo.DTO;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CUsuario.Modelo
{
    /// <summary>
    /// Interfaz para el servicio de usuarios
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Agregar un usuario
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a agregar</param>
        /// <returns>UsuarioDTO</returns>
        Task<UsuarioDTO> AgregarUsuario(UsuarioDTO usuarioDTO);

        /// <summary>
        /// Verifica si el nombre del usuario ya existe
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <returns>Booleano</returns>
        Task<bool> NombreUsuarioExistente(string nombreUsuario);

        /// <summary>
        /// Obtener un usuario por Id
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>UsuarioDTO</returns>
        Task<UsuarioDTO> ObtenerUsuarioPorId(int idUsuario);

        /// <summary>
        /// Autenticar al usuario con su nombre y contraseña
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a autenticar</param>
        /// <returns>UsuarioDTO</returns>
        Task<UsuarioDTO> AutenticarUsuario(UsuarioDTO usuarioDTO);
    }
}
