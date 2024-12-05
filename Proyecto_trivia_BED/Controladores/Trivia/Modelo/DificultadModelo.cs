using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    /// <summary>
    /// Modelo de dificultad
    /// </summary>
    public class DificultadModelo
    {
        private readonly TriviaContext _context;

        /// <summary>
        /// Constructor de DificultadModelo
        /// </summary>
        /// <param name="context">TriviaContext</param>
        public DificultadModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Obtener dificultad por nombre
        /// </summary>
        /// <param name="dificultadNombre">Nombre de la dificultad</param>
        /// <param name="externalWeb">Enum que identifica la API externa de la dificultad</param>
        /// <returns></returns>
        public async Task<Dificultad> ObtenerDificultadPorNombreAsync(string dificultadNombre, PaginasElegiblesEnum externalWeb)
        {
            return await _context.Dificultades.FirstOrDefaultAsync(dif => dif.NombreDificultad == dificultadNombre && dif.externalAPI == externalWeb);
        }

        /// <summary>
        /// Obtener dificultad por Id
        /// </summary>
        /// <param name="dificultadId">Id de la dificultad</param>
        /// <returns></returns>
        public async Task<Dificultad> ObtenerDificultadPorId(int dificultadId)
        {
            return await _context.Dificultades.FirstOrDefaultAsync(dif => dif.IdDificultad == dificultadId);
        }

        /// <summary>
        /// Obtener todas las dificultades de una API
        /// </summary>
        /// <param name="api">Enum que identifica la API externa de la dificultad</param>
        /// <returns>Lista de Dificultad</returns>
        public async Task<List<Dificultad>> ObtenerDificultadesAsync(PaginasElegiblesEnum api)
        {
            return await _context.Dificultades.Where(d => d.externalAPI == api).ToListAsync();
        }
    }
}
