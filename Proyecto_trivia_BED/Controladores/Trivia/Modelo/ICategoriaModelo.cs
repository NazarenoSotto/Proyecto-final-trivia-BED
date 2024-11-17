using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public interface ICategoriaModelo
    {
        public Task<bool> CategoriaExistenteAsync(string nombreCategoria);
        public Task<ECategoria> AgregarCategoriaAsync(ECategoria categoria);
        public Task<ECategoria> obtenerCategoriaPorNombreAsync(string categoriaNombre, PaginasElegiblesEnum externalWeb);
        public Task<ECategoria> obtenerCategoriaPorIdAsync(int categoriaId);
        public Task<List<ECategoria>> ObtenerCategoriasAsync(PaginasElegiblesEnum api);
    }
}
