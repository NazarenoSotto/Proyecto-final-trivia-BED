using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;

namespace Proyecto_trivia_BED.Controladores.CUsuario.Modelo
{
    /// <summary>
    /// Clase modelo de usuario
    /// </summary>
    public class UsuarioModelo
    {
        private readonly TriviaContext _context;

        /// <summary>
        /// Constructor de UsuarioModelo
        /// </summary>
        /// <param name="context">TriviaContext</param>
        public UsuarioModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Verifica si el nombre de usuario ya existe
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario</param>
        /// <returns></returns>
        public bool NombreUsuarioExistente(string nombreUsuario)
        {
            return _context.Usuarios.Any(u => u.NombreUsuario == nombreUsuario);
        }

        /// <summary>
        /// Agregar usuario
        /// </summary>
        /// <param name="usuario">Usuario a agregar</param>
        /// <returns></returns>
        public ContextoDB.Entidad.Usuario AgregarUsuario(ContextoDB.Entidad.Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        /// <summary>
        /// Obtener usuario por nombre
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario</param>
        /// <returns></returns>
        public ContextoDB.Entidad.Usuario ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        /// <summary>
        /// Obtener usuario por Id
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns></returns>
        public ContextoDB.Entidad.Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            return _context.Usuarios.Find(idUsuario);
        }
    }
}
