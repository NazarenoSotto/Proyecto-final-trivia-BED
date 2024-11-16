using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Proyecto_trivia_BED.ContextoDB.Entidad;
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
    public class OpenTDBAPI
    {
        private static HttpClient httpClient;
        public OpenTDBAPI(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("externalApiUrl:OpenTDBUrl"));
        }

        private static string GenerarUrl(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string baseUrl = "/api.php?";
            List<string> parametros = new List<string>();

            parametros.Add($"amount={pCantidad}");

            // Agregar "category" si se proporciona un valor
            if (pCategoriaId.HasValue)
            {
                parametros.Add($"category={pCategoriaId.Value}");
            }

            // Agregar "difficulty" si se proporciona un valor
            if (pDificultadId.HasValue)
            {
                parametros.Add($"difficulty={pDificultadId.Value}");
            }

            // Combinar la base de la URL con los parámetros usando '&' como separador
            return $"{baseUrl}{string.Join("&", parametros)}";
        }

        private async static List<EPregunta> ObtenerPreguntas(int pCantidad, int? pCategoriaId, int? pDificultadId)
        {
            string requestUrl = GenerarUrl(pCantidad, pCategoriaId, pDificultadId);
            HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(requestUrl);

            WebResponse mResponse = mRequest.GetResponse();
            List<EPregunta> lPreguntas = new List<EPregunta>();
            // Se obtiene los datos de respuesta
            using (Stream responseStream = mResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                // Se parsea la respuesta y se serializa a JSON a un objeto dynamic
                dynamic mResponseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());

                foreach (var bResponseItem in mResponseJSON.results)
                {
                    String mLaPregunta = HttpUtility.HtmlDecode(bResponseItem.question.ToString());
                    List<ERespuesta> lRespuestas = new List<ERespuesta>();

                    ERespuesta mRespCorrecta = new ERespuesta(HttpUtility.HtmlDecode(bResponseItem.correct_answer.ToString()), true);
                    lRespuestas.Add(mRespCorrecta);
                    foreach (var bRespInc in bResponseItem.incorrect_answers)
                    {
                        lRespuestas.Add(new ERespuesta(HttpUtility.HtmlDecode(bRespInc.ToString()), false));
                    }

                    EPregunta mPregunta = new EPregunta(mLaPregunta, pCategoriaId, pDificultadId, lRespuestas);
                    lPreguntas.Add(mPregunta);
                }

                return lPreguntas;
            }
        }


    }
}
