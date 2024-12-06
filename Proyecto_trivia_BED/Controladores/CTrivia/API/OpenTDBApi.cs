using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.CTrivia.API.DTO;
using Proyecto_trivia_BED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Proyecto_trivia_BED.Controladores.CTrivia.API.DTO.OpenTDBCategoriaResponseDTO;
using static Proyecto_trivia_BED.Controladores.CTrivia.API.DTO.OpenTDBResponseDTO;

namespace Proyecto_trivia_BED.Controladores.CTrivia.API
{
    /// <summary>
    /// Adapter para utilizar la API de OpenTDB
    /// </summary>
    public class OpenTDBAPI : ITriviaAPIAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly IEntityRepository<Categoria> _categoriaRepositorio;
        private readonly IEntityRepository<Dificultad> _dificultadRepositorio;

        /// <summary>
        /// Constructor de OpenTDBAPI
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="categoriaRepositorio">Repositorio de Categorias</param>
        /// <param name="dificultadRepositorio">Repositorio de Dificultades</param>
        public OpenTDBAPI(
            IConfiguration configuration,
            IEntityRepository<Categoria> categoriaRepositorio,
            IEntityRepository<Dificultad> dificultadRepositorio)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetValue<string>("externalApiUrl:OpenTDBUrl"))
            };
            _categoriaRepositorio = categoriaRepositorio ?? throw new ArgumentNullException(nameof(categoriaRepositorio));
            _dificultadRepositorio = dificultadRepositorio ?? throw new ArgumentNullException(nameof(dificultadRepositorio));
        }

        /// <summary>
        /// Genera la URL para obtener las preguntas con los parámetros requeridos
        /// </summary>
        private async Task<string> GenerarUrlAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string baseEndpoint = "/api.php?";
            List<string> parametros = new List<string>
            {
                $"amount={pCantidad}"
            };

            if (pCategoriaId.HasValue)
            {
                var categoria = await _categoriaRepositorio.GetByIdAsync(pCategoriaId.Value);
                if (categoria != null)
                {
                    parametros.Add($"category={categoria.WebId}");
                }
                else
                {
                    throw new ArgumentException("No se encontró la categoría requerida.");
                }
            }

            if (pDificultadId.HasValue)
            {
                var dificultad = await _dificultadRepositorio.GetByIdAsync(pDificultadId.Value);
                if (dificultad != null)
                {
                    parametros.Add($"difficulty={dificultad.NombreDificultad}");
                }
                else
                {
                    throw new ArgumentException("No se encontró la dificultad requerida.");
                }
            }

            parametros.Add("type=multiple");

            return $"{baseEndpoint}{string.Join("&", parametros)}";
        }

        /// <summary>
        /// Obtener categorías desde la API
        /// </summary>
        public async Task<List<Categoria>> ObtenerCategoriasAsync()
        {
            string baseEndpoint = "/api_category.php";
            List<Categoria> entityCategorias = new List<Categoria>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(baseEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var mResponseJSON = JsonConvert.DeserializeObject<OpenTDBCategoriaResponseDTO>(responseContent);

                    entityCategorias = mResponseJSON.trivia_categories.Select(c => new Categoria
                    {
                        NombreCategoria = c.name,
                        WebId = c.id,
                        externalAPI = PaginasElegiblesEnum.OpenTDB
                    }).ToList();
                }

                return entityCategorias;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtener preguntas desde la API
        /// </summary>
        public async Task<List<Pregunta>> ObtenerPreguntasAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string requestUrl = await GenerarUrlAsync(pCantidad, pCategoriaId, pDificultadId);
            List<Pregunta> lPreguntas = new List<Pregunta>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var mResponseJSON = JsonConvert.DeserializeObject<OpenTDBResponseDTO>(responseContent);

                    foreach (var bResponseItem in mResponseJSON.results)
                    {
                        string decodedQuestion = HttpUtility.HtmlDecode(bResponseItem.question);
                        List<Respuesta> respuestas = new List<Respuesta>
                        {
                            new Respuesta { SRespuesta = HttpUtility.HtmlDecode(bResponseItem.correct_answer), Correcta = true }
                        };

                        respuestas.AddRange(bResponseItem.incorrect_answers.Select(incorrect => new Respuesta
                        {
                            SRespuesta = HttpUtility.HtmlDecode(incorrect),
                            Correcta = false
                        }));

                        var categoria = await _categoriaRepositorio.GetAsync(c => c.NombreCategoria == HttpUtility.HtmlDecode(bResponseItem.category) &&
                            c.externalAPI == PaginasElegiblesEnum.OpenTDB);
                        var dificultad = await _dificultadRepositorio.GetAsync(d => d.NombreDificultad == HttpUtility.HtmlDecode(bResponseItem.difficulty) &&
                            d.externalAPI == PaginasElegiblesEnum.OpenTDB);

                        lPreguntas.Add(new Pregunta
                        {
                            LaPregunta = decodedQuestion,
                            Categoria = categoria.FirstOrDefault(),
                            Dificultad = dificultad.FirstOrDefault(),
                            Respuestas = respuestas
                        });
                    }
                }

                return lPreguntas;
            }
            catch
            {
                throw;
            }
        }
    }
}
