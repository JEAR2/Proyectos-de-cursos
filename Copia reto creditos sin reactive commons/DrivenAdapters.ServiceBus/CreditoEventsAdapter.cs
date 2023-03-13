using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.Model.Interfaces;
using DrivenAdapters.ServiceBus.Base;
using DrivenAdapters.ServiceBus.Entities;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using org.reactivecommons.api;
using System.Reflection;

namespace DrivenAdapters.ServiceBus
{
    public class CreditoEventsAdapter : AsyncGatewayAdapterBase, IClienteEventsRepository
    {
        private readonly IDirectAsyncGateway<CreditoEntity> _directAsyncGatewayCredito;
        private readonly IOptions<ConfiguradorAppSettings> _appSettings;

        public CreditoEventsAdapter(IDirectAsyncGateway<CreditoEntity> directAsyncGatewayCredito,
            IManageEventsUseCase manageEventsUseCase,
            IOptions<ConfiguradorAppSettings> appSettings)
            : base(manageEventsUseCase, appSettings)
        {
            _directAsyncGatewayCredito = directAsyncGatewayCredito;
            _appSettings = appSettings;
        }

        //Evento
        /// <summary>
        /// <see cref="IClienteEventsRepository.NotificarCreditoAsignadoAsync(Credito)"/>
        /// </summary>
        /// <param name="credito"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task NotificarCreditoAsignadoAsync(Credito credito)
        {
            string eventName = "Credito.Creado";
            CreditoEntity creditoAsignado = MapeoCredito(credito);

            await HandleSendEventAsync(_directAsyncGatewayCredito,
                credito.Id, creditoAsignado,
                _appSettings.Value.TopicoCreditos,
                eventName, MethodBase.GetCurrentMethod()!);
        }

        //Comando
        /// <summary>
        /// <see cref="IClienteEventsRepository.SolicitarNotificacionEmailCreditoAsignadoAsyn(Credito)"/>
        /// </summary>
        /// <param name="credito"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SolicitarNotificacionEmailCreditoAsignadoAsyn(Credito credito)
        {
            string commandName = "Credito.NotificarEmail";
            CreditoEntity creditoAsignado = MapeoCredito(credito);
            await HandleSendCommandAsync(_directAsyncGatewayCredito,
                credito.Id,
                creditoAsignado,
                _appSettings.Value.ColaNotificacionAsignacionCredito,
                commandName, MethodBase.GetCurrentMethod()!);
        }

        private static CreditoEntity MapeoCredito(Credito credito) => new(credito.Id, credito.Concepto, credito.Monto,
                credito.Cuotas, credito.ValorCuota, credito.CuotasPagadas, credito.CuotasPendientes, credito.Saldo, credito.FechaInicio,
                credito.FechaFin, credito.FechaProximaCuota);
    }
}