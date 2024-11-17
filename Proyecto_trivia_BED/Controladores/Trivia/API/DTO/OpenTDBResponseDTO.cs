using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.API.DTO
{
    public class OpenTDBResponseDTO
    {
        public List<OpenTDBResponseQuestionDTO> results;

        public class OpenTDBResponseQuestionDTO {
            public string type { get; set; }
            public string difficulty { get; set; }
            public string category { get; set; }
            public string question { get; set; }

            public string correct_answer { get; set; }

            public List<string> incorrect_answers { get; set; }
        }
}
}
