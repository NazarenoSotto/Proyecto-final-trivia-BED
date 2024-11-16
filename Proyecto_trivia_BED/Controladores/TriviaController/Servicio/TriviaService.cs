using Microsoft.AspNetCore.Mvc;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Trivia.API;
using Proyecto_trivia_BED.Controladores.Trivia.API.DTO;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.Servicio
{
    public class TriviaService
    {
        private readonly Dictionary<PaginasElegiblesEnum, IPreguntaAPIAdapter> _apiAdapters;
        private static PreguntaModelo _preguntaModelo;

        public TriviaService(IEnumerable<IPreguntaAPIAdapter> apiAdapters, PreguntaModelo preguntaModelo)
        {
            _apiAdapters = new Dictionary<PaginasElegiblesEnum, IPreguntaAPIAdapter>
                {
                    { PaginasElegiblesEnum.OpenTDB, apiAdapters.OfType<OpenTDBAPI>().FirstOrDefault() },
                };
            _preguntaModelo = preguntaModelo;
        }

        public async Task<List<PreguntaDTO>> ObtenerPreguntasDesdeAPIAsync(PaginasElegiblesEnum apiEnum, int cantidad, int? categoriaId, int? dificultadId)
        {
            try { 
                List<EPregunta> preguntasObtenidas = new List<EPregunta>();
                List<EPregunta> preguntasAgregadas = new List<EPregunta>();
                if (_apiAdapters.ContainsKey(apiEnum))
                {
                    preguntasObtenidas = await _apiAdapters[apiEnum].ObtenerPreguntasAsync(cantidad, categoriaId, dificultadId);
                } else
                {
                    throw new ArgumentException($"API no encontrada para '{apiEnum}'.");
                }

                if (preguntasObtenidas.Count > 0) {
                    preguntasAgregadas = await _preguntaModelo.GuardarPreguntasAsync(preguntasObtenidas);
                }

                List<PreguntaDTO> preguntasAgregadasDTO = MapearListaDePreguntasEntidadADTO(preguntasAgregadas);

                return preguntasAgregadasDTO;
            } catch (Exception ex)
            {
                throw;
            }
        }
        public PreguntaDTO mapearPreguntaEntidadADTO(EPregunta pregunta)
        {
            return new PreguntaDTO
            {
                IdPregunta = pregunta.IdPregunta,
                LaPregunta = pregunta.LaPregunta,
                Categoria = new CategoriaDTO
                {
                    IdCategoria = pregunta.Categoria.IdCategoria,
                    NombreCategoria = pregunta.Categoria.NombreCategoria,
                    WebId = pregunta.Categoria.WebId,
                },
                Dificultad = new DificultadDTO
                {
                    IdDificultad = pregunta.Dificultad.IdDificultad,
                    Valor = pregunta.Dificultad.Valor,
                    webId = pregunta.Dificultad.webId
                },
                Respuestas = pregunta.Respuestas.Select(r => new RespuestaDTO
                {
                    IdRespuesta = r.IdRespuesta,
                    TextoRespuesta = r.SRespuesta
                }).ToList()
            };
        }
        public List<PreguntaDTO> MapearListaDePreguntasEntidadADTO(List<EPregunta> preguntas)
        {
            return preguntas.Select(pregunta => mapearPreguntaEntidadADTO(pregunta)).ToList();
        }
    }
}
