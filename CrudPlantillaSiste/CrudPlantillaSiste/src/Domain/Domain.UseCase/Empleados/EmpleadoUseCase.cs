using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Empleados
{
    /// <summary>
    /// <see cref="IEmpleadoUseCase"/>
    /// </summary>
    public class EmpleadoUseCase : IEmpleadoUseCase
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        /// <summary>
        /// Inicialización de una nueva instancia de la clase <see cref="EmpleadoUseCase"/>
        /// </summary>
        /// <param name="empleadoRepository"></param>
        /// <param name="departamentoRepository"></param>
        public EmpleadoUseCase(IEmpleadoRepository empleadoRepository, IDepartamentoRepository departamentoRepository)
        {
            _empleadoRepository = empleadoRepository;
            _departamentoRepository = departamentoRepository;
        }

        /// <summary>
        /// <see cref="IEmpleadoUseCase.InsertarEmpleadoAsync(Empleado)"/>
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public async Task<Empleado> InsertarEmpleadoAsync(Empleado empleado)
        {
            Departamento departamento = await _departamentoRepository.ObtenerDepartamentoPorIdAsync(empleado.Departamento.Id);
            empleado.EstablecerDepartamento(departamento);
            return await _empleadoRepository.InsertarEmpleadoAsync(empleado);
        }

        /// <summary>
        /// <see cref="IEmpleadoUseCase.ObtenerEmpleadosAsync()"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Empleado>> ObtenerEmpleadosAsync()
        {
            return await _empleadoRepository.ObtenerEmpleadosAsync();
        }
    }
}