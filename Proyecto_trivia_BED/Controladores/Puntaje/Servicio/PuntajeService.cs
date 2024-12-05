using Proyecto_trivia_BED.ContextoDB.Entidad;
using Proyecto_trivia_BED.Controladores.Puntaje.Modelo.DTO;
using Proyecto_trivia_BED.Controladores.CUsuario.Modelo.DTO;
using Proyecto_trivia_BED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Controladores.Puntaje.Servicio
{
    /// <summary>
    /// Servicio para las funcionalidades de puntaje
    /// </summary>
    public class PuntajeService : IPuntajeService
    {
        private readonly IEntityRepository<ContextoDB.Entidad.Puntaje> _puntajeRepositorio;
        private readonly IEntityRepository<Usuario> _usuarioRepositorio;

        /// <summary>
        /// Constructor de PuntajeService
        /// </summary>
        /// <param name="puntajeRepositorio">Repositorio de puntajes</param>
        /// <param name="usuarioRepositorio">Repositorio de usuarios</param>
        public PuntajeService(IEntityRepository<ContextoDB.Entidad.Puntaje> puntajeRepositorio, IEntityRepository<Usuario> usuarioRepositorio)
        {
            _puntajeRepositorio = puntajeRepositorio ?? throw new ArgumentNullException(nameof(puntajeRepositorio));
            _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
        }

        /// <summary>
        /// Calcular el puntaje del usuario
        /// </summary>
        /// <param name="request">Datos necesarios para calcular el puntaje</param>
        /// <returns>PuntajeDTO</returns>
        public async Task<PuntajeDTO> CalcularPuntaje(CalculoPuntajeDTO request)
        {
            if (request.PreguntasEvaluadas == null || !request.PreguntasEvaluadas.Any())
                throw new ArgumentException("No hay preguntas evaluadas.");

            // Factores de cálculo
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

            // Calcular puntaje
            float valorPuntaje = ((float)cantCorrectas / cantPreguntas) * factorDificultad * factorTiempo;

            // Obtener usuario
            var usuario = await _usuarioRepositorio.GetByIdAsync(request.Usuario.IdUsuario);
            if (usuario == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            // Crear entidad de puntaje
            var puntajeEntidad = new ContextoDB.Entidad.Puntaje
            {
                Usuario = usuario,
                ValorPuntaje = valorPuntaje,
                Fecha = DateTime.Now,
                Tiempo = request.Tiempo
            };

            await _puntajeRepositorio.CreateAsync(puntajeEntidad);
            await _puntajeRepositorio.SaveChangesAsync();

            // Convertir a DTO
            return new PuntajeDTO
            {
                IdPuntaje = puntajeEntidad.IdPuntaje,
                Usuario = new UsuarioDTO
                {
                    IdUsuario = usuario.IdUsuario,
                    NombreUsuario = usuario.NombreUsuario
                },
                ValorPuntaje = valorPuntaje,
                Fecha = puntajeEntidad.Fecha,
                Tiempo = puntajeEntidad.Tiempo,
                CantidadPreguntas = cantPreguntas,
                CantidadCorrectas = cantCorrectas
            };
        }

        /// <summary>
        /// Obtener la lista de puntajes
        /// </summary>
        /// <returns>Lista de PuntajeDTO</returns>
        public async Task<List<PuntajeDTO>> ObtenerTodosLosPuntajes()
        {
            var puntajes = await _puntajeRepositorio.GetAllAsync();

            return puntajes
                .Select(p => new PuntajeDTO
                {
                    IdPuntaje = p.IdPuntaje,
                    Usuario = new UsuarioDTO
                    {
                        IdUsuario = p.Usuario.IdUsuario,
                        NombreUsuario = p.Usuario.NombreUsuario
                    },
                    ValorPuntaje = p.ValorPuntaje,
                    Fecha = p.Fecha,
                    Tiempo = p.Tiempo
                })
                .ToList();
        }
    }
}
