using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Repositories
{
    /// <summary>
    /// Clase EmpleadoRepository
    /// </summary>
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly IMongoCollection<EmpleadoEntity> _coleccionEmpleados;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public EmpleadoRepository(IContext context)
        {
            _coleccionEmpleados = context.Empleados;
        }

        /// <summary>
        /// <see cref="IEmpleadoRepository.InsertarEmpleadoAsync(Empleado)"/>
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public async Task<Empleado> InsertarEmpleadoAsync(Empleado empleado)
        {
            EmpleadoEntity empleadoEntity =
                new(empleado.Nombre, empleado.Apellido, empleado.Edad, empleado.Correo, empleado.Sexo, new DepartamentoEntity(empleado.Departamento?.Id ?? 0, empleado.Departamento?.Nombre, empleado.Departamento?.Descripcion));

            await _coleccionEmpleados.InsertOneAsync(empleadoEntity);
            return empleadoEntity.AsEntity();
        }

        /// <summary>
        /// <see cref="IEmpleadoRepository.ObtenerEmpleadosAsync"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Empleado>> ObtenerEmpleadosAsync()
        {
            IAsyncCursor<EmpleadoEntity> empleadoEntity =
                await _coleccionEmpleados.FindAsync(Builders<EmpleadoEntity>.Filter.Empty);

            List<Empleado> empleados = empleadoEntity.ToEnumerable()
                .Select(empleadoEntity => empleadoEntity.AsEntity()).ToList();
            return empleados;
        }

        /// <summary>
        /// <see cref="IEmpleadoRepository.EliminarEmppleado(int)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> EliminarEmpleado(string id)
        {
            await _coleccionEmpleados.DeleteOneAsync(empleado => empleado.Id.Equals(id));
            return true;
        }

        /// <summary>
        /// <see cref="IEmpleadoRepository.ActualizarEmpleado(string, Empleado)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public async Task<Empleado> ActualizarEmpleado(string id, Empleado empleado)
        {
            EmpleadoEntity empleadoEntity =
                new(id, empleado.Nombre, empleado.Apellido, empleado.Edad, empleado.Correo, empleado.Sexo, new DepartamentoEntity(empleado.Departamento?.Id ?? 0, empleado.Departamento?.Nombre, empleado.Departamento?.Descripcion));

            var filter = Builders<EmpleadoEntity>.Filter.Eq(adapter => adapter.Id, id);
            var result = await _coleccionEmpleados.ReplaceOneAsync(filter, empleadoEntity);
            if (result.ModifiedCount == 0)
            {
                throw new BusinessException("Error al actualizar el empleado", 500);
            }

            return empleadoEntity.AsEntity();
        }
    }
}