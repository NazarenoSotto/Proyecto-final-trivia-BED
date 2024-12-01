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
    /// Clase modelo de pregunta
    /// </summary>
    public class PreguntaModelo
    {
        private readonly TriviaContext _context;

        /// <summary>
        /// Constructor de PreguntaModelo
        /// </summary>
        /// <param name="context">TriviaContext</param>
        public PreguntaModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Guardar múltiples preguntas
        /// </summary>
        /// <param name="preguntas">Preguntas a guardar</param>
        /// <returns></returns>
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

        /// <summary>
        /// Obtener lista de preguntas
        /// </summary>
        /// <param name="categoriaId">Categoría de las preguntas</param>
        /// <param name="dificultadId">Dificultad de las preguntas</param>
        /// <param name="cantidad">Cantidad de preguntas</param>
        /// <returns></returns>
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
        /// <summary>
        /// Obtener una pregunta por Id con sus respuestas
        /// </summary>
        /// <param name="preguntaId">Id de la pregunta</param>
        /// <returns>EPregunta</returns>
        public async Task<EPregunta> ObtenerPreguntaConRespuestas(int preguntaId)
        {
            return await _context.Preguntas
                .Include(p => p.Respuestas)
                .FirstOrDefaultAsync(p => p.IdPregunta == preguntaId);
        }
        /// <summary>
        /// Guardar una pregunta
        /// </summary>
        /// <param name="pregunta">Pregunta a guardar</param>
        /// <returns></returns>
        public async Task GuardarPregunta(EPregunta pregunta)
        {
            await _context.Preguntas.AddAsync(pregunta);
            _context.SaveChanges();
        }
    }
}
