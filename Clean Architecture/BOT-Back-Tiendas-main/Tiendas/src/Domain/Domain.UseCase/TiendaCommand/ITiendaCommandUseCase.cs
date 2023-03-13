using Domain.Model.Entities.Request;
using System.Threading.Tasks;

namespace Domain.UseCase.TiendaCommand
{
    /// <summary>
    /// Tienda UseCase
    /// </summary>
    public interface ITiendaCommandUseCase
    {
        /// <summary>
        /// Notificar tienda creada
        /// </summary>
        /// <param name="tienda"></param>
        /// <returns></returns>
        Task NotificarTiendaCreada(TiendaRequest tienda);
    }
}