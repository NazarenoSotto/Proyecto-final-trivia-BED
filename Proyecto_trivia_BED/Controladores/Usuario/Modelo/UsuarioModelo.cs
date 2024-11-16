using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public class UsuarioModelo
    {
        private readonly TriviaContext _context;

        public UsuarioModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool NombreUsuarioExistente(string nombreUsuario)
        {
            return _context.Usuarios.Any(u => u.NombreUsuario == nombreUsuario);
        }

        public EUsuario AgregarUsuario(EUsuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public EUsuario ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        public EUsuario ObtenerUsuarioPorId(int idUsuario)
        {
            return _context.Usuarios.Find(idUsuario);
        }
    }
}
