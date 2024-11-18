using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public interface IUsuarioService
    {
        UsuarioDTO AgregarUsuario(UsuarioDTO usuarioDTO);
        bool NombreUsuarioExistente(string nombreUsuario);
        EUsuario ObtenerUsuarioPorId(int idUsuario);
        UsuarioDTO AutenticarUsuario(UsuarioDTO usuarioDTO);
    }
}
