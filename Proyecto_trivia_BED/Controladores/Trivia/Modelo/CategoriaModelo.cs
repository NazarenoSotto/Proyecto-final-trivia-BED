﻿using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public class CategoriaModelo: ICategoriaModelo
    {
        private readonly TriviaContext _context;

        public CategoriaModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CategoriaExistenteAsync(string nombreCategoria)
        {
            return await _context.Categorias.AnyAsync(u => u.NombreCategoria == nombreCategoria);
        }

        public async Task<ECategoria> AgregarCategoriaAsync(ECategoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<ECategoria> obtenerCategoriaPorNombreAsync(string categoriaNombre, PaginasElegiblesEnum externalWeb)
        {
            return await _context.Categorias.FirstOrDefaultAsync(cat => cat.NombreCategoria == categoriaNombre && cat.externalAPI == externalWeb);
        }

        public async Task<ECategoria> obtenerCategoriaPorIdAsync(int categoriaId)
        {
            return await _context.Categorias.FirstOrDefaultAsync(cat => cat.IdCategoria == categoriaId);
        }

        public async Task<List<ECategoria>> ObtenerCategoriasAsync(PaginasElegiblesEnum api)
        {
            return await _context.Categorias.Where(c => c.externalAPI == api).ToListAsync();
        }

        public async Task<List<ECategoria>> GuardarCategoriasAsync(List<ECategoria> categorias)
        {
            try
            {
                var categoriasExistentes = await _context.Categorias.Select(c => new { c.NombreCategoria, c.WebId ,c.externalAPI }).ToListAsync();

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
