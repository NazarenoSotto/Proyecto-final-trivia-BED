using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public class CategoriaModelo: ICategoriaModelo
    {
        private readonly TriviaContext _context;

        public CategoriaModelo(TriviaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool CategoriaExistente(string nombreCategoria)
        {
            return _context.Categorias.Any(u => u.NombreCategoria == nombreCategoria);
        }

        public ECategoria AgregarCategoria(ECategoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public ECategoria obtenerCategoriaPorIdExterna(int categoriaWebId, PaginasElegiblesEnum externalWeb)
        {
            return _context.Categorias.FirstOrDefault(cat => cat.IdWeb == categoriaWebId && cat.externalAPI == externalWeb);
        }

    }
}
