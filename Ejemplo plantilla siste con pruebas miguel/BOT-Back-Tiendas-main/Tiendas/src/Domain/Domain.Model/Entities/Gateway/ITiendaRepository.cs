using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// ITiendaRepository interface
    /// </summary>
    public interface ITiendaRepository
    {
        /// <summary>
        /// Obtener todas las tiendas
        /// </summary>
        /// <returns></returns>
        Task<List<Tienda>> ObtenerTiendasAsync();

        /// <summary>
        /// Crear tienda
        /// </summary>
        /// <param name="crearAlmacen"></param>
        /// <returns></returns>
        Task<Tienda> InsertarTiendaAsync(Tienda crearAlmacen);
    }
}