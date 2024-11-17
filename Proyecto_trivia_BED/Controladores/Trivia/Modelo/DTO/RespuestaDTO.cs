namespace Proyecto_trivia_BED.Controladores.Trivia.Modelo.DTO
{
    public class RespuestaDTO
    {
        /// <summary>
        /// Identificador de la respuesta
        /// </summary>
        public int IdRespuesta { get; set; }

        /// <summary>
        /// String que define la respuesta
        /// </summary>
        public string TextoRespuesta { get; set; }

        /// <summary>
        /// booleano que define si es la respuesta correcta
        /// </summary>
        public bool Correcta { get; set; }

        /// <summary>
        /// booleano que define si fue la respuesta seleccionada por el usuario
        /// </summary>
        public bool Seleccionada { get; set; }

    }
}