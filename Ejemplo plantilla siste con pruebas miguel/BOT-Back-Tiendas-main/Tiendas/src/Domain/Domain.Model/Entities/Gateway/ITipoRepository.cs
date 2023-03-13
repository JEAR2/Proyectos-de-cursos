using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// ITipoRepository interface
    /// </summary>
    public interface ITipoRepository
    {
        /// <summary>
        /// Obtener tipo por id
        /// </summary>
        /// <returns></returns>
        Task<Tipo> ObtenerTipoPorIdAsync(int id);
    }
}