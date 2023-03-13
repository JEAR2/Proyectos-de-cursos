using Domain.Model.Entities.Request;
using Domain.UseCase.Common;
using EntryPoints.ServiceBus.Base;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using org.reactivecommons.api;
using org.reactivecommons.api.domain;
using System.Reflection;

namespace EntryPoints.ServicesBus.Tienda
{
    public class TiendaEventSubscription : SubscriptionBase, ITiendaEventSubscription
    {
        private readonly IDirectAsyncGateway<TiendaRequest> _directAsyncGatewayTienda;
        private readonly IOptions<ConfiguradorAppSettings> _appSettings;

        private readonly IManageEventsUseCase _manageEventsUseCase;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="directAsyncGatewayTienda"></param>
        /// <param name="appSettings"></param>
        /// <param name="manageEventsUseCase"></param>
        public TiendaEventSubscription(IDirectAsyncGateway<TiendaRequest> directAsyncGatewayTienda,
            IOptions<ConfiguradorAppSettings> appSettings,
            IManageEventsUseCase manageEventsUseCase) : base(manageEventsUseCase, appSettings)
        {
            _manageEventsUseCase = manageEventsUseCase;

            _directAsyncGatewayTienda = directAsyncGatewayTienda;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Subscribe on command
        /// </summary>
        /// <returns></returns>
        public async Task SubscribeAsync()
        {
            await SubscribeOnEventAsync(_directAsyncGatewayTienda,
                                            _appSettings.Value.TopicoTiendas,
                                            _appSettings.Value.SubscripcionTopicoTiendas,
                                            ProcesarTiendaCreada,
                                            MethodBase.GetCurrentMethod()!,
                                            maxConcurrentCalls: 1);
        }

        /// <summary>
        /// Procesar Tienda Creada
        /// </summary>
        /// <param name="tiendaEvent"></param>
        /// <returns></returns>
        private async Task ProcesarTiendaCreada(DomainEvent<TiendaRequest> tiendaEvent) =>
            await HandleRequestAsync(async (tienda) =>
                {
                    //TODO: Aquí se podría llamar un use case si es necesario.
                    await _manageEventsUseCase.ConsoleLogAsync(tiendaEvent.name, tiendaEvent.eventId,
                        tiendaEvent.data, writeData: true);
                },
            MethodBase.GetCurrentMethod()!,
            Guid.NewGuid().ToString(),
            tiendaEvent);
    }
}