﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// IEmpleadoRepository interface
    /// </summary>
    public interface IEmpleadoRepository
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