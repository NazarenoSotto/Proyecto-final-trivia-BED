using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.CUsuario.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.CUsuario.Modelo
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Agregar un usuario
        /// </summary>
        /// <param name="usuarioDTO">Usuario a agregar</param>
        /// <returns>UsuarioDTO</returns>
        UsuarioDTO AgregarUsuario(UsuarioDTO usuarioDTO);
        /// <summary>
        /// Verifica si el nombre del usuario ya existe
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <returns>Booleano</returns>
        bool NombreUsuarioExistente(string nombreUsuario);
        /// <summary>
        /// Obtener un usuario por Id
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>Usuario</returns>
        ContextoDB.Entidad.Usuario ObtenerUsuarioPorId(int idUsuario);
        /// <summary>
        /// Autenticar al usuario con su nombre y contraseña
        /// </summary>
        /// <param name="usuarioDTO">Usuario a autenticar</param>
        /// <returns>UsuarioDTO</returns>
        UsuarioDTO AutenticarUsuario(UsuarioDTO usuarioDTO);
    }
}
