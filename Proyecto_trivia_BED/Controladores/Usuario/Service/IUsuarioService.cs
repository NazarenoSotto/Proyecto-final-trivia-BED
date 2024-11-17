using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public interface IUsuarioService
    {
        UsuarioDTO ConvertirADTO(EUsuario usuario);
        EUsuario ConvertirAEntidad(UsuarioDTO usuarioDTO);
        UsuarioDTO AgregarUsuario(UsuarioDTO usuarioDTO);
        bool NombreUsuarioExistente(string nombreUsuario);
        EUsuario ObtenerUsuarioPorNombre(string nombreUsuario);
        bool VerificarPassword(string passwordIngresada, string passwordAlmacenada);
        EUsuario ObtenerUsuarioPorId(int idUsuario);
        UsuarioDTO AutenticarUsuario(UsuarioDTO usuarioDTO);
    }
}
