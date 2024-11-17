using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public class PreguntaModelo
    {
        private readonly TriviaContext _context;

        public PreguntaModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<EPregunta>> GuardarPreguntas(List<EPregunta> preguntas)
        {
            try {

                var preguntasExistentes = await _context.Preguntas.Select(p => new { p.LaPregunta, p.Categoria.IdCategoria, p.Dificultad.IdDificultad }).ToListAsync();

                var preguntasNuevas = preguntas.Where(p =>
                   !preguntasExistentes.Any(e =>
                       e.LaPregunta == p.LaPregunta &&
                       e.IdCategoria == p.Categoria.IdCategoria &&
                       e.IdDificultad == p.Dificultad.IdDificultad))
                    .ToList();

                if (preguntasNuevas.Any())
                {
                    await _context.Preguntas.AddRangeAsync(preguntasNuevas);
                    await _context.SaveChangesAsync();
                }

                return preguntasNuevas;
            } catch
            {
                throw;
            }
        }

        public async Task<List<EPregunta>> ObtenerPreguntas(int categoriaId, int dificultadId, int cantidad)
        {
            return await _context.Preguntas
                .Where(p => p.Categoria.IdCategoria == categoriaId &&
                            p.Dificultad.IdDificultad == dificultadId)
                .OrderBy(p => Guid.NewGuid())
                .Take(cantidad)
                .Include(p => p.Categoria)
                .Include(p => p.Dificultad)
                .Include(p => p.Respuestas)
                .ToListAsync();
        }

        public async Task<EPregunta> ObtenerPreguntaConRespuestas(int preguntaId)
        {
            return await _context.Preguntas
                .Include(p => p.Respuestas)
                .FirstOrDefaultAsync(p => p.IdPregunta == preguntaId);
        }

        public async Task GuardarPregunta(EPregunta pregunta)
        {
            await _context.Preguntas.AddAsync(pregunta);
            _context.SaveChanges();
        }
    }
}
