using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.ContextoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Modelo
{
    public class PuntajeModelo
    {
        private readonly TriviaContext _context;

        public PuntajeModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public EPuntaje GuardarPuntaje(EPuntaje puntaje)
        {
            if (puntaje == null)
            {
                throw new ArgumentNullException(nameof(puntaje));
            }

            _context.Puntajes.Add(puntaje);
            _context.SaveChanges();

            return puntaje;
        }

        public async Task<List<EPuntaje>> ObtenerTodosLosPuntajes()
        {
            return await _context.Puntajes
                .Include(p => p.Usuario)
                .ToListAsync();
        }
    }
}
