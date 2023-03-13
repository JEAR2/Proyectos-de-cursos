using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Tiendas
{
    /// <summary>
    /// Tienda UseCase
    /// </summary>
    public interface ITiendaUseCase
    {
        /// <summary>
        /// Obtener todas las tiendas.
        /// </summary>
        /// <returns></returns>
        Task<List<Tienda>> ObtenerTiendas();

        /// <summary>
        /// Crear tienda
        /// </summary>
        /// <param name="tienda"></param>
        /// <returns></returns>
        Task<Tienda> CrearTienda(Tienda tienda);
    }
}