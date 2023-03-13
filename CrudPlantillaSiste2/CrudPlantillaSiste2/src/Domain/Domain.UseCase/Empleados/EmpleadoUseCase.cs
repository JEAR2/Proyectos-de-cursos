using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;
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
            if (empleado is null)
            {
                throw new BusinessException(TipoExcepcionNegocio.EmpleadoNoValido.GetDescription(), (int)TipoExcepcionNegocio.EmpleadoNoValido);
            }
            if (empleado.Departamento.Id < 1)
            {
                throw new BusinessException(TipoExcepcionNegocio.DepartamentoNoValido.GetDescription(), (int)TipoExcepcionNegocio.DepartamentoNoValido);
            }
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

        /// <summary>
        /// <see cref="IEmpleadoUseCase.EliminarEmpleado(string)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<bool> EliminarEmpleado(string id)
        {
            return await _empleadoRepository.EliminarEmpleado(id);
        }

        /// <summary>
        /// <see cref="IEmpleadoUseCase.ActualizarEmpleado(string, Empleado)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public async Task<Empleado> ActualizarEmpleado(string id, Empleado empleado)
        {
            Departamento departamento = await _departamentoRepository.ObtenerDepartamentoPorIdAsync(empleado.Departamento.Id);
            empleado.EstablecerDepartamento(departamento);
            return await _empleadoRepository.ActualizarEmpleado(id, empleado);
        }
    }
}