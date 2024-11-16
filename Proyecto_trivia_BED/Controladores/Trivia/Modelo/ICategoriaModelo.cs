using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public interface ICategoriaModelo
    {
        public bool CategoriaExistente(string nombreCategoria);

        public ECategoria AgregarCategoria(ECategoria categoria);
        public ECategoria obtenerCategoriaPorIdExterna(int categoriaWebId, PaginasElegiblesEnum externalWeb);
    }
}
