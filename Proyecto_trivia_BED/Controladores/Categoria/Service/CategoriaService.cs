using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public class UsuarioModelo : IUsuarioModelo
    {
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

        public EUsuario ConvertirAEntitidad(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return null;

            return new EUsuario
            {
                IdUsuario = usuarioDTO.IdUsuario,
                NombreUsuario = usuarioDTO.NombreUsuario,
                EsAdmin = usuarioDTO.EsAdmin
            };
        }
    }
}
