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

namespace Proyecto_trivia_BED.Controladores.Trivia.API
{
    public class OpenTDBAPI : ITriviaAPIAdapter
    {
        private static HttpClient _httpClient;
        private static CategoriaModelo _categoriaModelo;
        private static DificultadModelo _dificultadModelo;

        public OpenTDBAPI(IConfiguration configuration, CategoriaModelo categoriaModelo, DificultadModelo dificultadModelo)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("externalApiUrl:OpenTDBUrl"));
            _categoriaModelo = categoriaModelo;
            _dificultadModelo = dificultadModelo;
        }

        private static async Task<string> GenerarUrlAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string baseEndpoint = "/api.php?";
            List<string> parametros = new List<string>();

            parametros.Add($"amount={pCantidad}");

            // Agregar "category" si se proporciona un valor
            if (pCategoriaId.HasValue)
            {
                ECategoria categoria = await _categoriaModelo.obtenerCategoriaPorIdAsync((int)pCategoriaId);
                parametros.Add($"category={categoria.WebId}");
            }

            // Agregar "difficulty" si se proporciona un valor
            if (pDificultadId.HasValue)
            {
                EDificultad dificultad = await _dificultadModelo.obtenerDificultadPorId((int)pDificultadId);
                parametros.Add($"difficulty={dificultad.webId}");
            }

            // Combinar la base de la URL con los parámetros usando '&' como separador
            return $"{baseEndpoint}{string.Join("&", parametros)}";
        }

        public async Task<List<ECategoria>> ObtenerCategoriasAsync()
        {
            string baseEndpoint = "/api_category.php";
            string fullUrl = new Uri(_httpClient.BaseAddress, baseEndpoint).ToString();
            Console.WriteLine($"URL generada: {fullUrl}");

            List<ECategoria> entityCategorias = new List<ECategoria>();
            try
            {
                
                // Se obtiene los datos de categorias
                HttpResponseMessage response = await _httpClient.GetAsync(baseEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"responseContent: {responseContent}");
                    // Deserializar el contenido JSON en un objeto dynamic
                    OpenTDBCategoriaResponseDTO mResponseJSON = JsonConvert.DeserializeObject<OpenTDBCategoriaResponseDTO>(responseContent);
                    Console.WriteLine($"responseContent: {mResponseJSON.TriviaCategories.Count} categories found");

                    entityCategorias = mResponseJSON.TriviaCategories.Select(c => new ECategoria
                    (
                        c.Name,
                        c.Id,
                        PaginasElegiblesEnum.OpenTDB
                    )).ToList();

                    Console.WriteLine($"entityCategorias: {entityCategorias.Count}");
                }

                return entityCategorias;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<EPregunta>> ObtenerPreguntasAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string requestUrl = await GenerarUrlAsync(pCantidad, pCategoriaId, pDificultadId);

            try {

                List<EPregunta> lPreguntas = new List<EPregunta>();


                // Se obtiene los datos de respuesta
                    HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserializar el contenido JSON en un objeto dynamic
                    dynamic mResponseJSON = JsonConvert.DeserializeObject(responseContent);

                    foreach (OpenTDBResponseDTO bResponseItem in mResponseJSON.results)
                    {
                        String mLaPregunta = HttpUtility.HtmlDecode(bResponseItem.question.ToString());
                        List<ERespuesta> lRespuestas = new List<ERespuesta>();
                        ERespuesta mRespCorrecta = new ERespuesta(HttpUtility.HtmlDecode(bResponseItem.correct_answer.ToString()), true);
                        lRespuestas.Add(mRespCorrecta);
                        foreach (var bRespInc in bResponseItem.incorrect_answers)
                        {
                            lRespuestas.Add(new ERespuesta(HttpUtility.HtmlDecode(bRespInc.ToString()), false));
                        }

                        string nombreCategoria = HttpUtility.HtmlDecode(bResponseItem.category.ToString());
                        ECategoria categoriaPregunta = await _categoriaModelo.obtenerCategoriaPorNombreAsync(nombreCategoria, PaginasElegiblesEnum.OpenTDB);

                        string nombreDificultad = HttpUtility.HtmlDecode(bResponseItem.difficulty.ToString());
                        EDificultad dificultadPregunta = await _dificultadModelo.obtenerDificultadPorNombreAsync(nombreDificultad, PaginasElegiblesEnum.OpenTDB);
                        EPregunta mPregunta = new EPregunta(mLaPregunta, categoriaPregunta, dificultadPregunta, lRespuestas);
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
