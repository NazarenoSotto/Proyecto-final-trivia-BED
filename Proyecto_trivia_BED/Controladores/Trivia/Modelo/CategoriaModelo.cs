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
    /// Clase modelo de categoría
    /// </summary>
    public class CategoriaModelo
    {
        private readonly TriviaContext _context;
        /// <summary>
        /// Constructor de CategoriaModelo
        /// </summary>
        /// <param name="context">TriviaContext</param>
        public CategoriaModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Detecta si la categoría existe o no
        /// </summary>
        /// <param name="nombreCategoria">Nombre de la categoría</param>
        /// <returns>boolean</returns>
        public async Task<bool> CategoriaExistenteAsync(string nombreCategoria)
        {
            return await _context.Categorias.AnyAsync(u => u.NombreCategoria == nombreCategoria);
        }

        /// <summary>
        /// Agregar una categoría
        /// </summary>
        /// <param name="categoria">Categoría a agregar</param>
        /// <returns>ECategoría</returns>
        public async Task<ECategoria> AgregarCategoriaAsync(ECategoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        /// <summary>
        /// Obtener una categoría por nombre
        /// </summary>
        /// <param name="categoriaNombre">Nombre de la categoría</param>
        /// <param name="externalWeb">Web externa de la categoría</param>
        /// <returns>ECategoría</returns>
        public async Task<ECategoria> obtenerCategoriaPorNombreAsync(string categoriaNombre, PaginasElegiblesEnum externalWeb)
        {
            return await _context.Categorias.FirstOrDefaultAsync(cat => cat.NombreCategoria == categoriaNombre && cat.externalAPI == externalWeb);
        }

        /// <summary>
        /// Obtener una categoría por Id
        /// </summary>
        /// <param name="categoriaId">Id de la categoría</param>
        /// <returns>ECategoría</returns>
        public async Task<ECategoria> obtenerCategoriaPorIdAsync(int categoriaId)
        {
            return await _context.Categorias.FirstOrDefaultAsync(cat => cat.IdCategoria == categoriaId);
        }

        /// <summary>
        /// Obtener lista de categorías
        /// </summary>
        /// <param name="api">Enum que identifica la API externa de las categoría</param>
        /// <returns>Lista de ECategoría</returns>
        public async Task<List<ECategoria>> ObtenerCategoriasAsync(PaginasElegiblesEnum api)
        {
            return await _context.Categorias.Where(c => c.externalAPI == api).ToListAsync();
        }

        /// <summary>
        /// Guardar múltiples categorías
        /// </summary>
        /// <param name="categorias">Categorías a guardar</param>
        /// <returns>Lista de ECategoria</returns>
        public async Task<List<ECategoria>> GuardarCategoriasAsync(List<ECategoria> categorias)
        {
            try
            {
                var categoriasExistentes = await _context.Categorias.Select(c => new { c.NombreCategoria, c.WebId ,c.externalAPI }).ToListAsync();

                // Detecta si las categorías ya existen y deja sólo las categorías no existentes
                var categoriasNuevas = categorias.Where(c =>
                   !categoriasExistentes.Any(e =>
                       e.NombreCategoria == c.NombreCategoria &&
                       e.WebId == c.WebId &&
                       e.externalAPI == c.externalAPI))
                    .ToList();

                if (categoriasNuevas.Any())
                {
                    await _context.Categorias.AddRangeAsync(categoriasNuevas);
                    await _context.SaveChangesAsync();
                }

                return categoriasNuevas;
            }
            catch
            {
                throw;
            }
        }

    }
}
