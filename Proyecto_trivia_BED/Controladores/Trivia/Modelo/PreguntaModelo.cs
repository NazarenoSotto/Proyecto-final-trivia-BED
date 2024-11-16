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

        public async Task<List<EPregunta>> GuardarPreguntasAsync(List<EPregunta> preguntas)
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

        public List<EPregunta> ObtenerPreguntas(int categoriaId, int dificultadId, int cantidad)
        {
            return _context.Preguntas
                .Where(p => p.Categoria.IdCategoria == categoriaId &&
                            p.Dificultad.IdDificultad == dificultadId)
                .Take(cantidad)
                .ToList();
        }

        public void GuardarPreguntaManual(EPregunta pregunta)
        {
            _context.Preguntas.Add(pregunta);
            _context.SaveChanges();
        }
    }
}
