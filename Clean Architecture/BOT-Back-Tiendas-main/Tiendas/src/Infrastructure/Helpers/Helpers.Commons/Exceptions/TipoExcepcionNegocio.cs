using System.ComponentModel;

namespace Helpers.Commons.Exceptions
{
    /// <summary>
    /// ResponseError
    /// </summary>
    public enum TipoExcepcionNegocio
    {
        /// <summary>
        /// Tipo de exception no controlada
        /// </summary>
        [Description("Excepción de negocio no controlada")]
        ExceptionNoControlada = 555,

        /// <summary>
        /// Tipo de exception no controlada
        /// </summary>
        [Description("La tienda no puede ser nula")]
        TiendaInvalida = 100,

        /// <summary>
        /// Tipo de exception no controlada
        /// </summary>
        [Description("El tipo de la tienda no puede ser nulo")]
        TipoInvalido = 101,
    }
}