using Domain.Model.Entities.Request;
using Domain.UseCase.Common;
using Domain.UseCase.TiendaCommand;
using EntryPoints.ServiceBus.Base;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using org.reactivecommons.api;
using org.reactivecommons.api.domain;
using System.Reflection;

namespace EntryPoints.ServicesBus.Tienda
{
    public class TiendaCommandSubscription : SubscriptionBase, ITiendaCommandSubscription
    {
        private readonly IDirectAsyncGateway<TiendaRequest> _directAsyncGatewayTienda;
        private readonly IOptions<ConfiguradorAppSettings> _appSettings;

        private readonly ITiendaCommandUseCase _tiendaUseCase;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="directAsyncGatewayTienda"></param>
        /// <param name="appSettings"></param>
        /// <param name="tiendaUseCase"></param>
        /// <param name="manageEventsUseCase"></param>
        public TiendaCommandSubscription(IDirectAsyncGateway<TiendaRequest> directAsyncGatewayTienda,
            IOptions<ConfiguradorAppSettings> appSettings,
            ITiendaCommandUseCase tiendaUseCase,
            IManageEventsUseCase manageEventsUseCase) : base(manageEventsUseCase, appSettings)
        {
            _tiendaUseCase = tiendaUseCase;
            _directAsyncGatewayTienda = directAsyncGatewayTienda;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Subscribe on command
        /// </summary>
        /// <returns></returns>
        public async Task SubscribeAsync()
        {
            await SubscribeOnCommandAsync(_directAsyncGatewayTienda,
                                            _appSettings.Value.ColaNotificacionTiendas,
                                            NotificarTiendaCreadaAsync,
                                            MethodBase.GetCurrentMethod()!,
                                            maxConcurrentCalls: 1);
        }

        /// <summary>
        /// Notificar Tienda Creada
        /// </summary>
        /// <param name="tiendaCommand"></param>
        /// <returns></returns>
        private async Task NotificarTiendaCreadaAsync(Command<TiendaRequest> tiendaCommand) =>
            await HandleRequestAsync(async (tienda) =>
                {
                    await _tiendaUseCase.NotificarTiendaCreada(tienda);
                },
            MethodBase.GetCurrentMethod()!,
            Guid.NewGuid().ToString(),
            tiendaCommand);
    }
}