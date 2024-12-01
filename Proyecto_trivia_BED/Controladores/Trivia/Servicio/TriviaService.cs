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
    /// <summary>
    /// Servicio para las fucnionalidades de trivia
    /// </summary>
    public class TriviaService: ITriviaService
    {
        private readonly Dictionary<PaginasElegiblesEnum, ITriviaAPIAdapter> _apiAdapters;
        private static PreguntaModelo _preguntaModelo;
        private static CategoriaModelo _categoriaModelo;
        private static DificultadModelo _dificultadModelo;

        /// <summary>
        /// Constructor de TriviaService
        /// </summary>
        /// <param name="apiAdapters">IEnumerable<ITriviaAPIAdapter></param>
        /// <param name="preguntaModelo">PreguntaModelo</param>
        /// <param name="categoriaModelo">CategoriaModelo</param>
        /// <param name="dificultadModelo">DificultadModelo</param>
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

        /// <summary>
        /// Obtener preguntas desde la api
        /// </summary>
        /// <param name="apiEnum">Enumerable que representa la API</param>
        /// <param name="cantidad">Cantidad de preguntas</param>
        /// <param name="categoriaId">Categoría de las preguntas</param>
        /// <param name="dificultadId">Dificultad de las preguntas</param>
        /// <returns>Lista de PreguntaDTO</returns>
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

        /// <summary>
        /// Cargar categorías desde la API seleccionada
        /// </summary>
        /// <param name="apiEnum">Enumerable que representa la API</param>
        /// <returns>Lista de CategoriaDTO</returns>
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
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Convertir pregunta entidad a DTO
        /// </summary>
        /// <param name="pregunta">Pregunta entidad a mapear</param>
        /// <returns>PreguntaDTO</returns>
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
        /// <summary>
        /// Convertir lista de preguntas entidad a preguntas DTO
        /// </summary>
        /// <param name="preguntas">Preguntas entidad a convertir</param>
        /// <returns>Lista de PreguntaDTO</returns>
        private List<PreguntaDTO> MapearListaDePreguntasEntidadADTO(List<EPregunta> preguntas)
        {
            return preguntas.Select(pregunta => mapearPreguntaEntidadADTO(pregunta)).ToList();
        }

        /// <summary>
        /// Obtener lista de categorías
        /// </summary>
        /// <param name="api">Enumerable que representa la API</param>
        /// <returns>Lista de CategoriaDTO</returns>
        public async Task<List<CategoriaDTO>> ObtenerCategorias(PaginasElegiblesEnum api)
        {
            var categorias = await _categoriaModelo.ObtenerCategoriasAsync(api);
            return categorias.Select(c => new CategoriaDTO
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria
            }).ToList();
        }

        /// <summary>
        /// Obtener lista de dificultades
        /// </summary>
        /// <param name="api">Enumerable que representa la API</param>
        /// <returns></returns>
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

        /// <summary>
        /// Obtener lista de preguntas
        /// </summary>
        /// <param name="request">PreguntaRequestDTO</param>
        /// <returns>Lista de PreguntaDTO</returns>
        public async Task<List<PreguntaDTO>> ObtenerPreguntas(PreguntaRequestDTO request)
        {
            var preguntas = await _preguntaModelo.ObtenerPreguntas(request.CategoriaId, request.DificultadId, request.Cantidad);
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

        /// <summary>
        /// Verificar pregunta
        /// </summary>
        /// <param name="preguntaDTO">Pregunta a verificar</param>
        /// <returns>PreguntaDTO</returns>
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

        /// <summary>
        /// Guardar pregunta manual
        /// </summary>
        /// <param name="pregunta">Pregunta a guardar</param>
        /// <param name="api">Enumerable que representa la API</param>
        /// <returns>Booleano</returns>
        public async Task<bool> GuardarPreguntaManual(PreguntaDTO pregunta, PaginasElegiblesEnum api)
        {
            try {
                var categoria = await _categoriaModelo.obtenerCategoriaPorIdAsync(pregunta.Categoria.IdCategoria);

                if (categoria == null)
                {
                    throw new ArgumentException("idCategoría inválido");
                }

                var dificultad = await _dificultadModelo.ObtenerDificultadPorId(pregunta.Dificultad.IdDificultad);

                if (dificultad == null)
                {
                    throw new ArgumentException("idDificultad inválido");
                }

                var entidad = new EPregunta(
                    pregunta.LaPregunta,
                    categoria,
                    dificultad,
                    pregunta.Respuestas.Select(r => new ERespuesta
                    {
                        SRespuesta = r.TextoRespuesta,
                        Correcta = r.Correcta
                    }).ToList()
                );

                await _preguntaModelo.GuardarPregunta(entidad);
            return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Convertir categoría entidad a categoría DTO
        /// </summary>
        /// <param name="categoria">Categoría entidad a convertir</param>
        /// <returns>CategoríaDTO</returns>
        private CategoriaDTO mapearCategoriaEntidadADTO(ECategoria categoria)
        {
            return new CategoriaDTO
            {
                IdCategoria = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria,
                WebId = categoria.WebId
            };
        }

        /// <summary>
        /// Convertir una lista de categorías entidad a categorías DTO
        /// </summary>
        /// <param name="categorias">Categorías a convertir</param>
        /// <returns>Lista de CategoríaDTO</returns>
        private List<CategoriaDTO> MapearListaDeCategoriasEntidadADTO(List<ECategoria> categorias)
        {
            return categorias.Select(categoria => mapearCategoriaEntidadADTO(categoria)).ToList();
        }
    }
}
