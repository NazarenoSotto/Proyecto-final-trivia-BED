namespace Proyecto_trivia_BED.Controladores.CTrivia.Modelo.DTO
{
    /// <summary>
    /// DTO que representa los parámetros necesarios para obtener preguntas
    /// </summary>
    public class PreguntaRequestDTO
    {
        /// <summary>
        /// Id de la categoría de las preguntas
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Id de la dificultad de las preguntas
        /// </summary>
        public int DificultadId { get; set; }
        /// <summary>
        /// Cantidad de preguntas a obtener
        /// </summary>
        public int Cantidad { get; set; }

    }
}
