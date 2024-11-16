using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public class DificultadModelo: IDificultadModelo
    {
        private readonly TriviaContext _context;

        public DificultadModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EDificultad> obtenerDificultadPorNombreAsync(string dificultadNombre, PaginasElegiblesEnum externalWeb)
        {
            return await _context.Dificultades.FirstOrDefaultAsync(dif => dif.NombreDificultad == dificultadNombre && dif.externalAPI == externalWeb);
        }

        public async Task<EDificultad> obtenerDificultadPorId(int dificultadId)
        {
            return await _context.Dificultades.FirstOrDefaultAsync(dif => dif.IdDificultad == dificultadId);
        }
    }
}
