using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// Contrato de notificaciones de eventos de las tiendas
    /// </summary>
    public interface ITiendaEventsRepository
    {
        /// <summary>
        /// Notificar evento de creación de tienda.
        /// </summary>
        /// <param name="tienda"></param>
        /// <returns></returns>
        Task NotificarTiendaCreadaAsync(Tienda tienda);

        /// <summary>
        /// Solicitar Activación tienda.
        /// </summary>
        /// <param name="tienda"></param>
        /// <returns></returns>
        Task SolicitarNotificacionEmailTiendaCreadaAsync(Tienda tienda);
    }
}