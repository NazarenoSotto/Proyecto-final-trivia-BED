using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.ContextoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Modelo
{
    /// <summary>
    /// Clase modelo de puntaje
    /// </summary>
    public class PuntajeModelo
    {
        private readonly TriviaContext _context;

        /// <summary>
        /// Constructor de PuntajeModelo
        /// </summary>
        /// <param name="context">TriviaContext</param>
        public PuntajeModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Guardar un puntaje en la base de datos
        /// </summary>
        /// <param name="puntaje">Puntaje a guardar</param>
        /// <returns></returns>
        public ContextoDB.Entidad.Puntaje GuardarPuntaje(ContextoDB.Entidad.Puntaje puntaje)
        {
            if (puntaje == null)
            {
                throw new ArgumentNullException(nameof(puntaje));
            }

            _context.Puntajes.Add(puntaje);
            _context.SaveChanges();

            return puntaje;
        }

        /// <summary>
        /// Obtener todos los puntajes de manera descendente
        /// </summary>
        /// <returns></returns>
        public async Task<List<ContextoDB.Entidad.Puntaje>> ObtenerTodosLosPuntajes()
        {
            return await _context.Puntajes
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.ValorPuntaje)
                .ToListAsync();
        }
    }
}
