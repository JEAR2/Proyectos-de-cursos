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
                new(empleado.Nombre, empleado.Apellido, empleado.Edad, empleado.Correo, empleado.Sexo, new Departamento(empleado.Departamento.Nombre, empleado.Departamento.Descripcion));

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
    }
}