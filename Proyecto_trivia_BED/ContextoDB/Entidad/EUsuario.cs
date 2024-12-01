using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    /// <summary>
    /// Usuario de la trivia
    /// </summary>
    public class EUsuario
    {
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        [Key]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Contraseña cifrada del usuario
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Define si el usuario es administrador
        /// </summary>
        public bool EsAdmin { get; set; }

        /// <summary>
        /// Instanciar un Usuario
        /// </summary>
        /// <param name="pNombre">Nombre del usuario</param>
        /// <param name="pPassword">Contraseña del usuario</param>
        public EUsuario(string pNombre, string pPassword)
        {
            NombreUsuario = pNombre;
            Password = pPassword;
        }

        public EUsuario() { }
    }
}