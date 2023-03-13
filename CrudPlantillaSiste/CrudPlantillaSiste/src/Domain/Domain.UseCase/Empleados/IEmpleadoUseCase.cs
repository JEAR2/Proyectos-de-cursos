using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Empleados
{
    /// <summary>
    /// IEmpleadoUseCase interface
    /// </summary>
    public interface IEmpleadoUseCase
    {
        /// <summary>
        /// Obtener todos los empleados
        /// </summary>
        /// <returns></returns>
        Task<List<Empleado>> ObtenerEmpleadosAsync();

        /// <summary>
        /// Registrar un nuevo empleado
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        Task<Empleado> InsertarEmpleadoAsync(Empleado empleado);
    }
}