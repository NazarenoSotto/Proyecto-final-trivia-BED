using Proyecto_trivia_BED.ContextoDB;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public interface IDificultadModelo
    {
        public Task<EDificultad> obtenerDificultadPorNombreAsync(string dificultadNombre, PaginasElegiblesEnum externalWeb);
        public Task<EDificultad> obtenerDificultadPorId(int dificultadId);
    }
}
