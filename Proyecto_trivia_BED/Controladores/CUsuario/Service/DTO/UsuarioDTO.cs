using System.ComponentModel.DataAnnotations;

namespace Proyecto_trivia_BED.Controladores.CUsuario.Modelo.DTO
{
    /// <summary>
    /// DTO de usuario
    /// </summary>
    public class UsuarioDTO
    {
        /// <summary>
        /// Identificación del usuario
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres")]
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        /// <summary>
        /// Define si el usuario es admin
        /// </summary>
        public bool EsAdmin { get; set; }
    }
}
