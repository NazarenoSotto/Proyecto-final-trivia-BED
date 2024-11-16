using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public class IDificultadModelo
    {
        private readonly TriviaContext _context;

        public IDificultadModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
