using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo.DTO;
using System;
using Proyecto_trivia_BED.Controladores.Usuario.Modelo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Servicio
{
    public class PuntajeService : IPuntajeService
    {
        private readonly PuntajeModelo _puntajeModelo;
        private readonly IUsuarioService _usuarioServicio;

        public PuntajeService(PuntajeModelo puntajeModelo, IUsuarioService usuarioServicio)
        {
            _puntajeModelo = puntajeModelo ?? throw new ArgumentNullException(nameof(puntajeModelo));
            _usuarioServicio = usuarioServicio ?? throw new ArgumentNullException(nameof(usuarioServicio));
        }

        public PuntajeDTO CalcularPuntaje(CalculoPuntajeDTO request)
        {
            if (request.PreguntasEvaluadas == null || !request.PreguntasEvaluadas.Any())
                throw new ArgumentException("No hay preguntas evaluadas.");

            var dificultad = request.PreguntasEvaluadas.First().Dificultad;
            float factorDificultad = dificultad.Valor;

            int cantPreguntas = request.PreguntasEvaluadas.Count;
            int cantCorrectas = request.PreguntasEvaluadas.Count(p => p.Respuestas.Any(r => r.Correcta && r.Seleccionada));

            float calculoTiempo = (float)request.Tiempo / cantPreguntas;
            float factorTiempo = calculoTiempo switch
            {
                < 5 => 5f,
                < 20 => 3f,
                _ => 1f
            };

            float valorPuntaje = ((float)cantCorrectas / cantPreguntas) * factorDificultad * factorTiempo;

            var puntajeEntidad = new EPuntaje
            {
                Usuario = _usuarioServicio.ObtenerUsuarioPorId(request.Usuario.IdUsuario),
                ValorPuntaje = valorPuntaje,
                Fecha = DateTime.Now,
                Tiempo = request.Tiempo
            };

            var puntajeGuardado = _puntajeModelo.GuardarPuntaje(puntajeEntidad);

            return new PuntajeDTO
            {
                IdPuntaje = puntajeGuardado.IdPuntaje,
                Usuario = new UsuarioDTO
                {
                    IdUsuario = puntajeGuardado.Usuario.IdUsuario,
                    NombreUsuario = puntajeGuardado.Usuario.NombreUsuario
                },
                ValorPuntaje = puntajeGuardado.ValorPuntaje,
                Fecha = puntajeGuardado.Fecha,
                Tiempo = puntajeGuardado.Tiempo,
                CantidadPreguntas = cantPreguntas,
                CantidadCorrectas = cantCorrectas
            };
        }

        public async Task<List<PuntajeDTO>> ObtenerTodosLosPuntajes()
        {
            var puntajes = await _puntajeModelo.ObtenerTodosLosPuntajes();
            var puntajeDTOs = new List<PuntajeDTO>();
            foreach (var puntaje in puntajes)
            {
                puntajeDTOs.Add(new PuntajeDTO
                {
                    IdPuntaje = puntaje.IdPuntaje,
                    Usuario = new UsuarioDTO
                    {
                        IdUsuario = puntaje.Usuario.IdUsuario,
                        NombreUsuario = puntaje.Usuario.NombreUsuario
                    },
                    ValorPuntaje = puntaje.ValorPuntaje,
                    Fecha = puntaje.Fecha,
                    Tiempo = puntaje.Tiempo
                });
            }
            return puntajeDTOs;
        }
    }
}
