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
        [Description("La data del empleado no puede ser nula")]
        EmpleadoNoValido = 100,

        /// <summary>
        /// Tipo de exception no controladas
        /// </summary>
        [Description("El departamento al que pertenece el empleado  no puede ser nulo")]
        DepartamentoNoValido = 101,
    }
}