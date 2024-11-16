using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;
using Proyecto_trivia_BED.ContextoDB.Entidad;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public interface IUsuarioModelo
    {
        UsuarioDTO ConvertirADTO(EUsuario usuario);
        EUsuario ConvertirAEntitidad(UsuarioDTO usuarioDTO);

    }
}
