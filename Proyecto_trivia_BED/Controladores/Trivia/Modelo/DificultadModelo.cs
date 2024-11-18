using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public class DificultadModelo
    {
        private readonly TriviaContext _context;

        public DificultadModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EDificultad> ObtenerDificultadPorNombreAsync(string dificultadNombre, PaginasElegiblesEnum externalWeb)
        {
            return await _context.Dificultades.FirstOrDefaultAsync(dif => dif.NombreDificultad == dificultadNombre && dif.externalAPI == externalWeb);
        }

        public async Task<EDificultad> ObtenerDificultadPorId(int dificultadId)
        {
            return await _context.Dificultades.FirstOrDefaultAsync(dif => dif.IdDificultad == dificultadId);
        }

        public async Task<List<EDificultad>> ObtenerDificultadesAsync(PaginasElegiblesEnum api)
        {
            return await _context.Dificultades.Where(d => d.externalAPI == api).ToListAsync();
        }
    }
}
