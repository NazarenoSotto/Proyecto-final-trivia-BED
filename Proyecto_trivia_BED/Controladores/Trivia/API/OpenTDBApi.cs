using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Trivia.API.DTO;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Proyecto_trivia_BED.Controladores.Trivia.API.DTO.OpenTDBCategoriaResponseDTO;
using static Proyecto_trivia_BED.Controladores.Trivia.API.DTO.OpenTDBResponseDTO;

namespace Proyecto_trivia_BED.Controladores.Trivia.API
{
    /// <summary>
    /// Adapter para utilizar la API de OpenTDB
    /// </summary>
    public class OpenTDBAPI : ITriviaAPIAdapter
    {
        private static HttpClient _httpClient;
        private static CategoriaModelo _categoriaModelo;
        private static DificultadModelo _dificultadModelo;

        /// <summary>
        /// Constructor de OpenTDBAPI
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="categoriaModelo">CategoriaModelo</param>
        /// <param name="dificultadModelo">DificultadModelo</param>
        public OpenTDBAPI(IConfiguration configuration, CategoriaModelo categoriaModelo, DificultadModelo dificultadModelo)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("externalApiUrl:OpenTDBUrl"));
            _categoriaModelo = categoriaModelo;
            _dificultadModelo = dificultadModelo;
        }

        /// <summary>
        /// Genera la url para obtener las preguntas con los parámetros requeridos
        /// </summary>
        /// <param name="pCantidad">Cantidad de preguntas</param>
        /// <param name="pCategoriaId">Id de categoría de las preguntas</param>
        /// <param name="pDificultadId">Id de dificultad</param>
        /// <returns>string</returns>
        private static async Task<string> GenerarUrlAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string baseEndpoint = "/api.php?";
            List<string> parametros = new List<string>();

            parametros.Add($"amount={pCantidad}");

            // Agregar "category" si se proporciona un valor
            if (pCategoriaId.HasValue)
            {
                Categoria categoria = await _categoriaModelo.obtenerCategoriaPorIdAsync((int)pCategoriaId);
                if (categoria != null) { 
                    parametros.Add($"category={categoria.WebId}");
                } else
                {
                    throw new ArgumentException("No se encontró la categoría requerida");
                }
            }

            // Agregar "difficulty" si se proporciona un valor
            if (pDificultadId.HasValue)
            {
                Dificultad dificultad = await _dificultadModelo.ObtenerDificultadPorId((int)pDificultadId);
                if (dificultad != null) {
                    parametros.Add($"difficulty={dificultad.NombreDificultad}");
                }
                else
                {
                    throw new ArgumentException("No se encontró la dificultad requerida");
                }
            }

            parametros.Add("type=multiple");

            // Combinar la base de la URL con los parámetros usando '&' como separador
            return $"{baseEndpoint}{string.Join("&", parametros)}";
        }

        /// <summary>
        /// Obtener categorías desde la  API
        /// </summary>
        /// <returns>Lista de ECategoría</returns>
        public async Task<List<Categoria>> ObtenerCategoriasAsync()
        {
            string baseEndpoint = "/api_category.php";

            List<Categoria> entityCategorias = new List<Categoria>();
            try
            {
                
                // Se obtiene los datos de categorias
                HttpResponseMessage response = await _httpClient.GetAsync(baseEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    // Deserializar el contenido JSON en un objeto dynamic
                    OpenTDBCategoriaResponseDTO mResponseJSON = JsonConvert.DeserializeObject<OpenTDBCategoriaResponseDTO>(responseContent);

                    entityCategorias = mResponseJSON.trivia_categories.Select(c => new Categoria
                    (
                        c.name,
                        c.id,
                        PaginasElegiblesEnum.OpenTDB
                    )).ToList();
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
        /// <param name="cantidad">Cantidad de preguntas a obtener</param>
        /// <param name="categoriaId">Categoría de las preguntas</param>
        /// <param name="dificultadId">Dificultades de las preguntas</param>
        /// <returns>Lista de preguntas</returns>
        public async Task<List<Pregunta>> ObtenerPreguntasAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string requestUrl = await GenerarUrlAsync(pCantidad, pCategoriaId, pDificultadId);

            string fullUrl = new Uri(_httpClient.BaseAddress, requestUrl).ToString();

            // Registrar la URL generada
            try {
                List<Pregunta> lPreguntas = new List<Pregunta>();

                // Se obtiene los datos de respuesta
                    HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserializar el contenido JSON en un objeto dynamic
                    OpenTDBResponseDTO mResponseJSON = JsonConvert.DeserializeObject<OpenTDBResponseDTO>(responseContent);

                    foreach (OpenTDBResponseQuestionDTO bResponseItem in mResponseJSON.results)
                    {
                        String mLaPregunta = HttpUtility.HtmlDecode(bResponseItem.question.ToString());
                        List<Respuesta> lRespuestas = new List<Respuesta>();
                        Respuesta mRespCorrecta = new Respuesta(HttpUtility.HtmlDecode(bResponseItem.correct_answer.ToString()), true);
                        lRespuestas.Add(mRespCorrecta);
                        foreach (var bRespInc in bResponseItem.incorrect_answers)
                        {
                            lRespuestas.Add(new Respuesta(HttpUtility.HtmlDecode(bRespInc.ToString()), false));
                        }

                        string nombreCategoria = HttpUtility.HtmlDecode(bResponseItem.category.ToString());
                        Categoria categoriaPregunta = await _categoriaModelo.obtenerCategoriaPorNombreAsync(nombreCategoria, PaginasElegiblesEnum.OpenTDB);

                        string nombreDificultad = HttpUtility.HtmlDecode(bResponseItem.difficulty.ToString());
                        Dificultad dificultadPregunta = await _dificultadModelo.ObtenerDificultadPorNombreAsync(nombreDificultad, PaginasElegiblesEnum.OpenTDB);
                        Pregunta mPregunta = new Pregunta(mLaPregunta, categoriaPregunta, dificultadPregunta, lRespuestas);
                        lPreguntas.Add(mPregunta);
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
