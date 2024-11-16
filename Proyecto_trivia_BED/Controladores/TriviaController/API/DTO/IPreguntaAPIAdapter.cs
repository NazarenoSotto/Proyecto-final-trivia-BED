﻿using Proyecto_trivia_BED.ContextoDB.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Trivia.API.DTO
{
    public interface IPreguntaAPIAdapter
    {
        public Task<List<EPregunta>> ObtenerPreguntasAsync(int cantidad, int? categoriaId, int? dificultadId);

        public Task<List<EPregunta>> ObtenerCategoriasAsync(int cantidad, int? categoriaId, int? dificultadId);
    }
}
