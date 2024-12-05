using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Puntaje obtenido por el usuario
    /// </summary>
    public class EPuntaje
    {
        /// <summary>
        /// Identificador del puntaje
        /// </summary>
        [Key]
        public int IdPuntaje { get; set; }
        /// <summary>
        /// Usuario relacionado al puntaje
        /// </summary>
        public Usuario Usuario { get; set; }
        /// <summary>
        /// Valor del puntaje
        /// </summary>
        public float ValorPuntaje { get; set; }
        /// <summary>
        /// Fecha de obtención del puntaje
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Tiempo de resolución de la trivia
        /// </summary>
        public int Tiempo { get; set; }

        /// <summary>
        /// Crear una nueva instancia de EPuntaje
        /// </summary>
        /// <param name="pUsuario">Usuario relacionado al puntaje</param>
        /// <param name="pValor">Valor del puntaje</param>
        /// <param name="pFecha">Fecha de obtención del puntaje</param>
        /// <param name="pTiempo">Tiempo de resolución de la trivia</param>
        public EPuntaje(Usuario pUsuario, float pValor, DateTime pFecha, int pTiempo)
        {
            Usuario = pUsuario;
            ValorPuntaje = pValor;
            Fecha = pFecha;
            Tiempo = pTiempo;
        }

        public EPuntaje() { }
    }
}