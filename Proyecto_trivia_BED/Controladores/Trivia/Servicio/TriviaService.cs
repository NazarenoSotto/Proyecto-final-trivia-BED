using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class TriviaService: ITriviaService
    {
        private readonly Dictionary<PaginasElegiblesEnum, ITriviaAPIAdapter> _apiAdapters;
        private static PreguntaModelo _preguntaModelo;
        private static CategoriaModelo _categoriaModelo;
        private static DificultadModelo _dificultadModelo;

        public TriviaService(
            IEnumerable<ITriviaAPIAdapter> apiAdapters, 
            PreguntaModelo preguntaModelo, 
            CategoriaModelo categoriaModelo,
            DificultadModelo dificultadModelo)
        {
            _apiAdapters = new Dictionary<PaginasElegiblesEnum, ITriviaAPIAdapter>
                {
                    { PaginasElegiblesEnum.OpenTDB, apiAdapters.OfType<OpenTDBAPI>().FirstOrDefault() },
                };
            _preguntaModelo = preguntaModelo;
            _categoriaModelo = categoriaModelo;
            _dificultadModelo = dificultadModelo;
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
                    preguntasAgregadas = await _preguntaModelo.GuardarPreguntas(preguntasObtenidas);
                }

                List<PreguntaDTO> preguntasAgregadasDTO = MapearListaDePreguntasEntidadADTO(preguntasAgregadas);

                return preguntasAgregadasDTO;
            } catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CategoriaDTO>> CargarCategoriasDesdeAPIAsync(PaginasElegiblesEnum apiEnum)
        {
            try
            {
                List<ECategoria> categoriasObtenidas = new List<ECategoria>();
                List<ECategoria> categoriasAgregadas = new List<ECategoria>();
                Console.WriteLine($"_apiAdapters.count: {_apiAdapters.Count.ToString()}");
                if (_apiAdapters.ContainsKey(apiEnum))
                {
                    categoriasObtenidas = await _apiAdapters[apiEnum].ObtenerCategoriasAsync();
                }
                else

                {
                    throw new ArgumentException($"API no encontrada para '{apiEnum}'.");
                }

                if (categoriasObtenidas.Count > 0)
                {
                    categoriasAgregadas = await _categoriaModelo.GuardarCategoriasAsync(categoriasObtenidas);
                }

                List<CategoriaDTO> categoriasAgregadasDTO = MapearListaDeCategoriasEntidadADTO(categoriasAgregadas);

                return categoriasAgregadasDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private PreguntaDTO mapearPreguntaEntidadADTO(EPregunta pregunta)
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

        private List<PreguntaDTO> MapearListaDePreguntasEntidadADTO(List<EPregunta> preguntas)
        {
            return preguntas.Select(pregunta => mapearPreguntaEntidadADTO(pregunta)).ToList();
        }

        public async Task<List<CategoriaDTO>> ObtenerCategorias(PaginasElegiblesEnum api)
        {
            var categorias = await _categoriaModelo.ObtenerCategoriasAsync(api);
            return categorias.Select(c => new CategoriaDTO
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria
            }).ToList();
        }

        public async Task<List<DificultadDTO>> ObtenerDificultades(PaginasElegiblesEnum api)
        {
            var dificultades = await _dificultadModelo.ObtenerDificultadesAsync(api);
            return dificultades.Select(d => new DificultadDTO
            {
                IdDificultad = d.IdDificultad,
                NombreDificultad = d.NombreDificultad,
                Valor = d.Valor
            }).ToList();
        }

        public async Task<List<PreguntaDTO>> ObtenerPreguntas(PreguntaRequestDTO request)
        {
            Console.WriteLine($"request: {JsonConvert.SerializeObject(request)}");
            var preguntas = await _preguntaModelo.ObtenerPreguntas(request.CategoriaId, request.DificultadId, request.Cantidad);
            Console.WriteLine($"preguntas: {JsonConvert.SerializeObject(preguntas)}");
            return preguntas.Select(p => new PreguntaDTO
            {
                IdPregunta = p.IdPregunta,
                LaPregunta = p.LaPregunta,
                Categoria = new CategoriaDTO
                {
                    IdCategoria = p.Categoria.IdCategoria,
                    NombreCategoria = p.Categoria.NombreCategoria
                },
                Dificultad = new DificultadDTO
                {
                    IdDificultad = p.Dificultad.IdDificultad,
                    NombreDificultad = p.Dificultad.NombreDificultad,
                    Valor = p.Dificultad.Valor
                },
                Respuestas = p.Respuestas.Select(r => new RespuestaDTO
                {
                    IdRespuesta = r.IdRespuesta,
                    TextoRespuesta = r.SRespuesta
                }).ToList()
            }).ToList();
        }

        public async Task<PreguntaDTO> VerificarPregunta(PreguntaDTO preguntaDTO)
        {
            var pregunta = await _preguntaModelo.ObtenerPreguntaConRespuestas(preguntaDTO.IdPregunta);
            if (pregunta == null)
            {
                throw new InvalidOperationException("La pregunta no existe.");
            }

            var respuestaCorrectaId = pregunta.Respuestas.FirstOrDefault(r => r.Correcta)?.IdRespuesta;
            foreach (var respuesta in preguntaDTO.Respuestas)
            {
                respuesta.Correcta = respuesta.IdRespuesta == respuestaCorrectaId;
            }

            return preguntaDTO;
        }

        public bool GuardarPreguntaManual(PreguntaDTO pregunta)
        {
            var entidad = new EPregunta(
                pregunta.LaPregunta,
                new ECategoria { IdCategoria = pregunta.Categoria.IdCategoria },
                new EDificultad { IdDificultad = pregunta.Dificultad.IdDificultad },
                pregunta.Respuestas.Select(r => new ERespuesta
                {
                    SRespuesta = r.TextoRespuesta,
                    Correcta = r.Correcta
                }).ToList()
            );

            _preguntaModelo.GuardarPreguntaManual(entidad);
            return true;
        }

        private CategoriaDTO mapearCategoriaEntidadADTO(ECategoria categoria)
        {
            return new CategoriaDTO
            {
                IdCategoria = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria,
                WebId = categoria.WebId
            };
        }
        private List<CategoriaDTO> MapearListaDeCategoriasEntidadADTO(List<ECategoria> categorias)
        {
            return categorias.Select(categoria => mapearCategoriaEntidadADTO(categoria)).ToList();
        }
    }
}
