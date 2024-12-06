using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.CTrivia.API.DTO
{
    /// <summary>
    /// DTO de la respuesta de OpenTDB al consultar por preguntas
    /// </summary>
    public class OpenTDBResponseDTO
    {
        /// <summary>
        /// Lista de preguntas de OpenTDB
        /// </summary>
        public List<OpenTDBResponseQuestionDTO> results;

        /// <summary>
        /// pregunta de OpenTDB
        /// </summary>
        public class OpenTDBResponseQuestionDTO {
            /// <summary>
            /// Tipo de pregunta
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// Dificultad de la pregunta
            /// </summary>
            public string difficulty { get; set; }
            /// <summary>
            /// Categoría de la pregunta
            /// </summary>
            public string category { get; set; }
            /// <summary>
            /// Texto de la pregunta
            /// </summary>
            public string question { get; set; }
            /// <summary>
            /// Respuesta correcta de la pregunta
            /// </summary>
            public string correct_answer { get; set; }
            /// <summary>
            /// Lista de respuestas incorrectas de la pregunta
            /// </summary>
            public List<string> incorrect_answers { get; set; }
        }
}
}
