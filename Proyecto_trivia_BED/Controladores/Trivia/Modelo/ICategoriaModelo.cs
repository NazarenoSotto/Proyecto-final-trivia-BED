using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public class ICategoriaModelo
    {
        private readonly TriviaContext _context;

        public ICategoriaModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


    }
}
