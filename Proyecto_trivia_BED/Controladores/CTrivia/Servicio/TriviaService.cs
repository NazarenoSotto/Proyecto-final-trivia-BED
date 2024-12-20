﻿using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.CTrivia.API.DTO;
using Proyecto_trivia_BED.Controladores.CTrivia.Modelo.DTO;
using Proyecto_trivia_BED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CTrivia.Servicio
{
    /// <summary>
    /// Servicio para las funcionalidades de trivia
    /// </summary>
    public class TriviaService : ITriviaService
    {
        private readonly IEntityRepository<Pregunta> _preguntaRepositorio;
        private readonly IEntityRepository<Categoria> _categoriaRepositorio;
        private readonly IEntityRepository<Dificultad> _dificultadRepositorio;
        private readonly ITriviaAPIAdapter _apiAdapter;

        /// <summary>
        /// Constructor de TriviaService
        /// </summary>
        /// <param name="preguntaRepositorio">Repositorio de preguntas</param>
        /// <param name="categoriaRepositorio">Repositorio de categorías</param>
        /// <param name="dificultadRepositorio">Repositorio de dificultades</param>
        /// <param name="apiAdapter">Adapter de la API de trivia</param>
        public TriviaService(
            IEntityRepository<Pregunta> preguntaRepositorio,
            IEntityRepository<Categoria> categoriaRepositorio,
            IEntityRepository<Dificultad> dificultadRepositorio,
            ITriviaAPIAdapter apiAdapter)
        {
            _preguntaRepositorio = preguntaRepositorio ?? throw new ArgumentNullException(nameof(preguntaRepositorio));
            _categoriaRepositorio = categoriaRepositorio ?? throw new ArgumentNullException(nameof(categoriaRepositorio));
            _dificultadRepositorio = dificultadRepositorio ?? throw new ArgumentNullException(nameof(dificultadRepositorio));
            _apiAdapter = apiAdapter ?? throw new ArgumentNullException(nameof(apiAdapter));
        }

        /// <summary>
        /// Obtiene listado de Preguntas
        /// </summary>
        /// <param name="categoriaId">id de la categoria</param>
        /// <param name="dificultadId">id de la dificultad</param>
        /// <param name="cantidad">cantidad de preguntas</param>
        /// <returns>Lista de PreguntaDTO</returns>
        public async Task<List<PreguntaDTO>> ObtenerPreguntas(int categoriaId, int dificultadId, int cantidad)
        {
            var preguntas = await _preguntaRepositorio.GetAsync(
                where: p => p.Categoria.IdCategoria == categoriaId && p.Dificultad.IdDificultad == dificultadId,
                includes: "Categoria,Dificultad,Respuestas"
            );

            return preguntas.OrderBy(x => Guid.NewGuid()).Take(cantidad).Select(MapearPreguntaADTO).ToList();
        }

        /// <summary>
        /// Agregar Pregunta Manual con Respuestas
        /// </summary>
        /// <param name="preguntaDTO">pregunta con respuestas</param>
        /// <returns>Bool que indica si pudo agregarse</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> AgregarPreguntaManual(PreguntaDTO preguntaDTO)
        {
            var categoria = await _categoriaRepositorio.GetByIdAsync(preguntaDTO.Categoria.IdCategoria);
            var dificultad = await _dificultadRepositorio.GetByIdAsync(preguntaDTO.Dificultad.IdDificultad);

            if (categoria == null || dificultad == null)
                throw new InvalidOperationException("Categoría o dificultad no encontrada.");

            var preguntaEntidad = new Pregunta
            {
                LaPregunta = preguntaDTO.LaPregunta,
                Categoria = categoria,
                Dificultad = dificultad,
                Respuestas = preguntaDTO.Respuestas.Select(r => new Respuesta
                {
                    SRespuesta = r.TextoRespuesta,
                    Correcta = r.Correcta
                }).ToList()
            };

            await _preguntaRepositorio.CreateAsync(preguntaEntidad);
            await _preguntaRepositorio.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Verificar la respuesta seleccionada
        /// </summary>
        /// <param name="preguntaDTO">pregunta con respuestas</param>
        /// <returns>PreguntaDTO</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<PreguntaDTO> VerificarPregunta(PreguntaDTO preguntaDTO)
        {
            var preguntas = await _preguntaRepositorio.GetAsync(where: p => p.IdPregunta == preguntaDTO.IdPregunta, includes: "Categoria,Dificultad,Respuestas");
            var pregunta = preguntas.First();

            if (pregunta == null)
                throw new InvalidOperationException("Pregunta no encontrada.");

            var respuestaCorrectaId = pregunta.Respuestas.FirstOrDefault(r => r.Correcta)?.IdRespuesta;

            foreach (var respuesta in preguntaDTO.Respuestas)
            {
                respuesta.Correcta = respuesta.IdRespuesta == respuestaCorrectaId;
            }

            return preguntaDTO;
        }

        /// <summary>
        /// Obtener lista de preguntas desde API web
        /// </summary>
        /// <param name="api">a qué API se consulta</param>
        /// <param name="cantidad">la cantidad que se solicita</param>
        /// <param name="categoriaId">id de la categoria</param>
        /// <param name="dificultadId">id de la dificultad</param>
        /// <returns>Lista de PreguntaDTO</returns>
        public async Task<List<PreguntaDTO>> ObtenerPreguntasDesdeAPIAsync(PaginasElegiblesEnum api, int cantidad, int? categoriaId, int? dificultadId)
        {
            // Obtener preguntas desde la API externa
            var preguntasExternas = await _apiAdapter.ObtenerPreguntasAsync(cantidad, categoriaId, dificultadId);

            foreach (var preguntaExterna in preguntasExternas)
            {
                // Buscar si la pregunta ya existe en la base de datos
                var preguntaExistente = (await _preguntaRepositorio.GetAsync(
                    where: p => p.LaPregunta == preguntaExterna.LaPregunta &&
                                p.Categoria.IdCategoria == preguntaExterna.Categoria.IdCategoria &&
                                p.Dificultad.IdDificultad == preguntaExterna.Dificultad.IdDificultad,
                    includes: "Categoria,Dificultad"
                )).FirstOrDefault();

                if (preguntaExistente == null)
                {
                    await _preguntaRepositorio.CreateAsync(preguntaExterna);
                }
            }

            await _preguntaRepositorio.SaveChangesAsync();

            return preguntasExternas.Select(p => new PreguntaDTO
            {
                LaPregunta = p.LaPregunta,
                Categoria = new CategoriaDTO
                {
                    IdCategoria = p.Categoria.IdCategoria,
                    NombreCategoria = p.Categoria.NombreCategoria
                },
                Dificultad = new DificultadDTO
                {
                    IdDificultad = p.Dificultad.IdDificultad,
                    NombreDificultad = p.Dificultad.NombreDificultad
                },
                Respuestas = p.Respuestas.Select(r => new RespuestaDTO
                {
                    TextoRespuesta = r.SRespuesta,
                    Correcta = r.Correcta
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// Obtener lista de categorias
        /// </summary>
        /// <param name="api">API que se consumirá</param>
        /// <returns>Lista de CategoriaDTO</returns>
        public async Task<List<CategoriaDTO>> ObtenerCategorias(PaginasElegiblesEnum api)
        {
            var categorias = await _categoriaRepositorio.GetAsync(c => c.externalAPI == api);

            return categorias.Select(c => new CategoriaDTO
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria,
                WebId = c.WebId
            }).ToList();
        }

        /// <summary>
        /// Obtener lista de categorias desde la web
        /// </summary>
        /// <param name="api">API que se consumirá</param>
        /// <returns>Lista de CategoriaDTO</returns>
        public async Task<List<CategoriaDTO>> CargarCategoriasDesdeAPIAsync(PaginasElegiblesEnum api)
        {
            var categoriasExternas = await _apiAdapter.ObtenerCategoriasAsync();
            var categoriasExistentes = await _categoriaRepositorio.GetAsync(c => c.externalAPI == api);

            // Filtrar las categorías externas para identificar las nuevas
            var nuevasCategorias = categoriasExternas.Where(c =>
                !categoriasExistentes.Any(e =>
                    e.NombreCategoria == c.NombreCategoria &&
                    e.WebId == c.WebId &&
                    e.externalAPI == api)
            ).ToList();

            // Crear las categorías nuevas en la base de datos
            foreach (var nuevaCategoria in nuevasCategorias)
            {
                await _categoriaRepositorio.CreateAsync(new Categoria
                {
                    NombreCategoria = nuevaCategoria.NombreCategoria,
                    WebId = nuevaCategoria.WebId,
                    externalAPI = api
                });
            }

            await _categoriaRepositorio.SaveChangesAsync();

            var categoriasActualizadas = await _categoriaRepositorio.GetAsync(c => c.externalAPI == api);

            return categoriasActualizadas.Select(c => new CategoriaDTO
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria,
                WebId = c.WebId
            }).ToList();
        }

        /// <summary>
        /// Obtener lista de Dificultades
        /// </summary>
        /// <param name="api">api que consumirá</param>
        /// <returns>Lista de DificultadDTO</returns>
        public async Task<List<DificultadDTO>> ObtenerDificultades(PaginasElegiblesEnum api)
        {
            var dificultades = await _dificultadRepositorio.GetAsync(d => d.externalAPI == api);

            return dificultades.Select(d => new DificultadDTO
            {
                IdDificultad = d.IdDificultad,
                NombreDificultad = d.NombreDificultad,
                Valor = d.Valor
            }).ToList();
        }

        /// <summary>
        /// Mapea la Pregunta a DTO
        /// </summary>
        /// <param name="pregunta">pregunta a mapear</param>
        /// <returns>PreguntaDTO</returns>
        private PreguntaDTO MapearPreguntaADTO(Pregunta pregunta)
        {
            return new PreguntaDTO
            {
                IdPregunta = pregunta.IdPregunta,
                LaPregunta = pregunta.LaPregunta,
                Categoria = new CategoriaDTO
                {
                    IdCategoria = pregunta.Categoria.IdCategoria,
                    NombreCategoria = pregunta.Categoria.NombreCategoria
                },
                Dificultad = new DificultadDTO
                {
                    IdDificultad = pregunta.Dificultad.IdDificultad,
                    NombreDificultad = pregunta.Dificultad.NombreDificultad,
                    Valor = pregunta.Dificultad.Valor,
                },
                Respuestas = pregunta.Respuestas.Select(r => new RespuestaDTO
                {
                    IdRespuesta = r.IdRespuesta,
                    TextoRespuesta = r.SRespuesta,
                    Correcta = r.Correcta
                }).ToList()
            };
        }
    }
}
