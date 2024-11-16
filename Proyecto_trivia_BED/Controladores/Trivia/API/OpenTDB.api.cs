using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Trivia.API.DTO;
using Proyecto_trivia_BED.Controladores.Trivia.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Proyecto_trivia_BED.Controladores.Trivia.API
{
    public class OpenTDBAPI : IPreguntaAPIAdapter
    {
        private static HttpClient httpClient;
        private static CategoriaModelo _categoriaModelo;
        private static DificultadModelo _dificultadModelo;

        public OpenTDBAPI(IConfiguration configuration, CategoriaModelo categoriaModelo, DificultadModelo dificultadModelo)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("externalApiUrl:OpenTDBUrl"));
            _categoriaModelo = categoriaModelo;
            _dificultadModelo = dificultadModelo;
        }

        private static async Task<string> GenerarUrlAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string baseUrl = "/api.php?";
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
            return $"{baseUrl}{string.Join("&", parametros)}";
        }

        public Task<List<EPregunta>> ObtenerCategoriasAsync(int cantidad, int? categoriaId, int? dificultadId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EPregunta>> ObtenerPreguntasAsync(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string requestUrl = await GenerarUrlAsync(pCantidad, pCategoriaId, pDificultadId);
            HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(requestUrl);

            WebResponse mResponse = mRequest.GetResponse();
            List<EPregunta> lPreguntas = new List<EPregunta>();
            // Se obtiene los datos de respuesta
            using (Stream responseStream = mResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                // Se parsea la respuesta y se serializa a JSON a un objeto dynamic
                dynamic mResponseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());

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

                return lPreguntas;
            }
        }

    }
}
