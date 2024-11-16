using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    public class EUsuario
    {
        [Key]

        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public string Password { get; set; }

        public bool EsAdmin { get; set; }


        public EUsuario(string pNombre, string pPassword)
        {
            NombreUsuario = pNombre;
            Password = pPassword;
        }

        public EUsuario() { }
    }
}