using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo
{
    public interface IPreguntaModelo
    {
        public void GuardarPreguntaManual(EPregunta pregunta);
        public Task<List<EPregunta>> GuardarPreguntas(List<EPregunta> preguntas);
        public Task<List<EPregunta>> ObtenerPreguntas(int categoriaId, int dificultadId, int cantidad);
        public Task<EPregunta> ObtenerPreguntaConRespuestas(int preguntaId);
    }
}
