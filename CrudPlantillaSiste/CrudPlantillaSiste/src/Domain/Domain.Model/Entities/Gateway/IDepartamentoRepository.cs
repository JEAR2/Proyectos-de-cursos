using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// IDepartamentoRepository interface
    /// </summary>
    public interface IDepartamentoRepository
    {
        /// <summary>
        /// Obtener un departamento en especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Departamento> ObtenerDepartamentoPorIdAsync(int id);
    }
}