using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;

namespace Proyecto_trivia_BED.Controladores.Usuario.Modelo
{
    public class DificultadModelo
    {
        private readonly TriviaContext _context;

        public DificultadModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public EDificultad obtenerDificultadPorValor(int dificultadValue)
        //{
        //    return _context.Dificultades.FirstOrDefault(dif => dif.IdD == categoriaId);
        //}
    }
}
