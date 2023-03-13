using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.UseCase.Common;
using DrivenAdapter.ServicesBus.Base;
using DrivenAdapter.ServicesBus.Entities;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using org.reactivecommons.api;
using System.Reflection;

namespace DrivenAdapter.ServicesBus
{
    public class TiendaEventsAdapter : AsyncGatewayAdapterBase, ITiendaEventsRepository
    {
        private readonly IDirectAsyncGateway<TiendaEntity> _directAsyncGatewayTienda;
        private readonly IOptions<ConfiguradorAppSettings> _appSettings;

        public TiendaEventsAdapter(IDirectAsyncGateway<TiendaEntity> directAsyncGatewayTienda,
            IManageEventsUseCase manageEventsUseCase,
            IOptions<ConfiguradorAppSettings> appSettings)
            : base(manageEventsUseCase, appSettings)
        {
            _directAsyncGatewayTienda = directAsyncGatewayTienda;
            _appSettings = appSettings;
        }

        #region Eventos

        /// <summary>
        /// <see cref="ITiendaEventsRepository.NotificarTiendaCreadaAsync(Tienda)"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task NotificarTiendaCreadaAsync(Tienda tienda)
        {
            string eventName = "Tienda.Creada";

            TiendaEntity tiendaCreada = MapeoTienda(tienda);

            await HandleSendEventAsync(_directAsyncGatewayTienda, tienda.Id,
               tiendaCreada, _appSettings.Value.TopicoTiendas, eventName,
               MethodBase.GetCurrentMethod()!);
        }

        //HACK: Forma sencilla sin usar el adaptador base (AsyncGatewayAdapterBase)
        /// <summary>
        /// <see cref="ITiendaEventsRepository.NotificarTiendaCreadaAsync(Tienda)"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        //public async Task NotificarTiendaCreadaAsync(Tienda tienda)
        //{
        //    string eventName = "Tienda.Creada";
        //    TiendaEntity tiendaCreada = MapeoTienda(tienda);

        //    DomainEvent<TiendaEntity> domainService =
        //        new(eventName, tienda.Id, tiendaCreada);

        //    await _directAsyncGatewayTienda.SendEvent(_appSettings.Value.TopicoTiendas, domainService);
        //}

        #endregion Eventos

        #region Commands

        /// <summary>
        /// <see cref="ITiendaEventsRepository.SolicitarNotificacionEmailTiendaCreadaAsync(Tienda)"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task SolicitarNotificacionEmailTiendaCreadaAsync(Tienda tienda)
        {
            string commandName = "Tienda.NotificarEmail";
            TiendaEntity tiendaCreada = MapeoTienda(tienda);

            await HandleSendCommandAsync(_directAsyncGatewayTienda, tienda.Id,
               tiendaCreada, _appSettings.Value.ColaNotificacionTiendas, commandName,
               MethodBase.GetCurrentMethod()!);
        }

        #endregion Commands

        /// <summary>
        /// Obtener objeto TiendaEntity
        /// </summary>
        /// <param name="tienda"></param>
        /// <returns></returns>
        private static TiendaEntity MapeoTienda(Tienda tienda) => new(tienda.Id, tienda.Nombre, idTipo: tienda.Tipo.Id,
                tienda.Direccion, ubicacion: new UbicacionEntity(tienda.Ubicacion.Latitud, tienda.Ubicacion.Longitud),
                tienda.Celular, tienda.Correo);
    }
}