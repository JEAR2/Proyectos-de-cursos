using AutoMapper.Data;
using Domain.Model.Entities.Request;
using Domain.UseCase.Common;
using Domain.UseCase.TiendaCommand;
using DrivenAdapters.Mongo;
using EntryPoints.ServicesBus.Tienda;
using org.reactivecommons.api;
using org.reactivecommons.api.impl;
using Tiendas.AppServices.Messaging.Automapper;

namespace Tiendas.AppServices.Messaging.Extensions
{
    /// <summary>
    /// Service Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Método para registrar AutoMapper
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(cfg =>
            {
                cfg.AddDataReaderMapping();
            }, typeof(ConfigurationProfile));

        /// <summary>
        /// Método para registrar Mongo
        /// </summary>
        /// <param name="services">services.</param>
        /// <param name="connectionString">connection string.</param>
        /// <param name="db">database.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterMongo(this IServiceCollection services, string connectionString, string db) =>
                                    services.AddSingleton<IContext>(provider => new Context(connectionString, db));

        /// <summary>
        /// Método para registrar los servicios
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region UseCases

            services.AddTransient<ITiendaCommandUseCase, TiendaCommandUseCase>();
            services.AddTransient<IManageEventsUseCase, ManageEventsUseCase>();

            #endregion UseCases

            return services;
        }

        /// <summary>
        /// RegisterSubscriptions
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection RegisterSubscriptions(this IServiceCollection services)
        {
            services.AddTransient<ITiendaCommandSubscription, TiendaCommandSubscription>();
            services.AddTransient<ITiendaEventSubscription, TiendaEventSubscription>();

            return services;
        }

        /// <summary>
        /// RegisterAsyncGateways
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBusConn"></param>
        public static IServiceCollection RegisterAsyncGateways(this IServiceCollection services,
                string serviceBusConn)
        {
            services.RegisterAsyncGateway<TiendaRequest>(serviceBusConn);
            return services;
        }

        /// <summary>
        /// Register Gateway
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="services"></param>
        /// <param name="serviceBusConn"></param>
        private static void RegisterAsyncGateway<TEntity>(this IServiceCollection services, string serviceBusConn) =>
                services.AddSingleton<IDirectAsyncGateway<TEntity>>(new DirectAsyncGatewayServiceBus<TEntity>(serviceBusConn));
    }
}