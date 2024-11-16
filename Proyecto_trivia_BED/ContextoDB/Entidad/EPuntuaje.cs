using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.ContextoDB.Entidad
{
    public class Puntaje
    {
        [Key]

        public int IdPuntaje { get; set; }

        public EUsuario Usuario { get; set; }

        public float ValorPuntaje { get; set; }

        public DateTime Fecha { get; set; }

        public int Tiempo { get; set; }

        public Puntaje(EUsuario pUsuario, float pValor, DateTime pFecha, int pTiempo)
        {
            Usuario = pUsuario;
            ValorPuntaje = pValor;
            Fecha = pFecha;
            Tiempo = pTiempo;

        }

        public Puntaje() { }
    }
}