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
        /// Los datos del crédito no llegan
        /// </summary>
        [Description("La data del crédito no puede ser nula")]
        CreditoNoValido = 100,

        /// <summary>
        /// Los datos del cliente no llegan
        /// </summary>
        [Description("La data del cliente no puede ser nula")]
        ClienteNoValido = 101,
    }
}